using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus;

internal sealed class MenuDesignAPI : IMenuDesignAPI
{
    private readonly MenuConfiguration configuration;
    private readonly IMenuBuilderAPI builder;
    private readonly Action<MenuOptionScrollStyle> setScrollStyle;
    // private readonly Action<MenuOptionTextStyle> setTextStyle;

    public MenuDesignAPI( MenuConfiguration configuration, IMenuBuilderAPI builder, Action<MenuOptionScrollStyle> setScrollStyle/*, Action<MenuOptionTextStyle> setTextStyle*/ )
    {
        this.configuration = configuration;
        this.builder = builder;
        this.setScrollStyle = setScrollStyle;
        // this.setTextStyle = setTextStyle;
    }

    public IMenuBuilderAPI SetMenuTitle( string? title = null )
    {
        configuration.Title = title ?? "Menu";
        return builder;
    }

    public IMenuBuilderAPI SetMenuTitleVisible( bool visible = true )
    {
        configuration.HideTitle = !visible;
        return builder;
    }

    public IMenuBuilderAPI SetMenuFooterVisible( bool visible = true )
    {
        configuration.HideFooter = !visible;
        return builder;
    }

    public IMenuBuilderAPI SetMaxVisibleItems( int count = 5 )
    {
        configuration.MaxVisibleItems = count;
        return builder;
    }

    public IMenuBuilderAPI EnableAutoAdjustVisibleItems()
    {
        configuration.AutoIncreaseVisibleItems = true;
        return builder;
    }

    public IMenuBuilderAPI DisableAutoAdjustVisibleItems()
    {
        configuration.AutoIncreaseVisibleItems = false;
        return builder;
    }

    public IMenuBuilderAPI SetGlobalScrollStyle( MenuOptionScrollStyle style )
    {
        setScrollStyle(style);
        return builder;
    }

    // public IMenuBuilderAPI SetGlobalOptionTextStyle( MenuOptionTextStyle style )
    // {
    //     setTextStyle(style);
    //     return builder;
    // }
}