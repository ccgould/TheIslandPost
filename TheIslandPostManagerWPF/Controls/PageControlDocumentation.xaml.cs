// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheIslandPostManagerWPF.ViewModels;
using TheIslandPostManagerWPF.Views;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.Controls;

public class PageControlDocumentation : Control
{
    public static readonly DependencyProperty ShowProperty = DependencyProperty.RegisterAttached(
        "Show",
        typeof(bool),
        typeof(FrameworkElement),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender)
    );

    public static readonly DependencyProperty DocumentationTypeProperty = DependencyProperty.RegisterAttached(
        "DocumentationType",
        typeof(Type),
        typeof(FrameworkElement),
        new FrameworkPropertyMetadata(null)
    );

    public static bool GetShow(FrameworkElement target) => (bool)target.GetValue(ShowProperty);

    public static void SetShow(FrameworkElement target, bool show) => target.SetValue(ShowProperty, show);

    public static Type? GetDocumentationType(FrameworkElement target) =>
        (Type?)target.GetValue(DocumentationTypeProperty);

    public static void SetDocumentationType(FrameworkElement target, Type type) =>
        target.SetValue(DocumentationTypeProperty, type);

    public static readonly DependencyProperty NavigationViewProperty = DependencyProperty.Register(
        nameof(NavigationView),
        typeof(INavigationView),
        typeof(PageControlDocumentation),
        new FrameworkPropertyMetadata(null)
    );

    public static readonly DependencyProperty IsDocumentationLinkVisibleProperty =
        DependencyProperty.Register(
            nameof(IsDocumentationLinkVisible),
            typeof(Visibility),
            typeof(PageControlDocumentation),
            new FrameworkPropertyMetadata(Visibility.Collapsed)
        );

    public static readonly DependencyProperty TemplateButtonCommandProperty = DependencyProperty.Register(
        nameof(TemplateButtonCommand),
        typeof(ICommand),
        typeof(PageControlDocumentation),
        new PropertyMetadata(null)
    );

    public INavigationView? NavigationView
    {
        get => (INavigationView)GetValue(NavigationViewProperty);
        set => SetValue(NavigationViewProperty, value);
    }

    public Visibility IsDocumentationLinkVisible
    {
        get => (Visibility)GetValue(IsDocumentationLinkVisibleProperty);
        set => SetValue(IsDocumentationLinkVisibleProperty, value);
    }

    public ICommand TemplateButtonCommand => (ICommand)GetValue(TemplateButtonCommandProperty);

    public PageControlDocumentation()
    {
        Loaded += static (sender, _) => ((PageControlDocumentation)sender).OnLoaded();
        Unloaded += static (sender, _) => ((PageControlDocumentation)sender).OnUnloaded();

        SetValue(
            TemplateButtonCommandProperty,
            new CommunityToolkit.Mvvm.Input.AsyncRelayCommand<string>(OnClick)
        );
    }

    private FrameworkElement? _page;

    private void OnLoaded()
    {
        if (NavigationView is null)
            throw new ArgumentNullException(nameof(NavigationView));

        NavigationView.Navigated += NavigationViewOnNavigated;
    }

    private void OnUnloaded()
    {
        NavigationView!.Navigated -= NavigationViewOnNavigated;
        _page = null;
    }

    private void NavigationViewOnNavigated(NavigationView sender, NavigatedEventArgs args)
    {
        IsDocumentationLinkVisible = Visibility.Collapsed;

        if (args.Page is not FrameworkElement page || !GetShow(page))
        {
            Visibility = Visibility.Collapsed;
            return;
        }

        _page = page;
        Visibility = Visibility.Visible;

        if (GetDocumentationType(page) is not null)
        {
            IsDocumentationLinkVisible = Visibility.Visible;
        }
    }

    private async Task OnClick(string? param)
    {
        if (string.IsNullOrWhiteSpace(param) || _page is null)
        {
            return;
        }

        // TODO: Refactor switch
        if (param == "theme")
        {
            SwitchThemes();
            return;
        }

        var dataPage = _page.DataContext as DataPage;
        var viewModel = dataPage.ViewModel;

        switch(param)
        {
            case "selectAll":
                viewModel.Service.SelectAllImages();
                break;
            case "deselectAll":
                viewModel.Service.DeSelectAllImages();
                break;
            case "printAll":
                viewModel.Service.PrintAllImages();
                break;
            case "deleteAll":
                await viewModel.Service.DeleteAllImages();
                break;
            case "completeOrder":
                await viewModel.FinializeOrder();
                break;
            case "cancelOrder":
                await viewModel.CancelOrder();
                break;
        }
    }

    private static string CreateUrlForGithub(Type pageType, ReadOnlySpan<char> fileExtension)
    {
        const string baseUrl = "https://github.com/lepoco/wpfui/tree/main/src/Wpf.Ui.Gallery/";
        const string baseNamespace = "Wpf.Ui.Gallery";

        var pageFullNameWithoutBaseNamespace = pageType.FullName.AsSpan().Slice(baseNamespace.Length + 1);

        Span<char> pageUrl = stackalloc char[pageFullNameWithoutBaseNamespace.Length];
        pageFullNameWithoutBaseNamespace.CopyTo(pageUrl);

        for (int i = 0; i < pageUrl.Length; i++)
        {
            if (pageUrl[i] == '.')
            {
                pageUrl[i] = '/';
            }
        }

        return string.Concat(baseUrl, pageUrl, fileExtension);
    }

    private static string CreateUrlForDocumentation(Type type)
    {
        const string baseUrl = "https://wpfui.lepo.co/api/";

        return string.Concat(baseUrl, type.FullName, ".html");
    }

    private static void SwitchThemes()
    {
        var currentTheme = Wpf.Ui.Appearance.ApplicationThemeManager.GetAppTheme();

        Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
            currentTheme == Wpf.Ui.Appearance.ApplicationTheme.Light
                ? Wpf.Ui.Appearance.ApplicationTheme.Dark
                : Wpf.Ui.Appearance.ApplicationTheme.Light
        );
    }
}
