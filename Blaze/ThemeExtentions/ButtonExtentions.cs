using System.Windows;
using System.Windows.Media;
using Blaze.Core.Extentions;
using MahApps.Metro.IconPacks;

namespace Blaze.Extentions
{
    public class Codicons : IExtention<PackIconCodiconsKind, Codicons>
    {
    }

    public class Coolicons : IExtention<PackIconCooliconsKind, Coolicons>
    {
    }
    
    public class CornerRadiusExtention : IExtention<CornerRadius, CornerRadiusExtention>
    {
    }

    public class IconHeight : IExtention<int, IconHeight>
    {
    }

    public class IsMouseOverColor : IExtention<Brush, IsMouseOverColor>
    {
    }

    public class IsPressedColor : IExtention<Brush, IsPressedColor>
    {
    }
}
