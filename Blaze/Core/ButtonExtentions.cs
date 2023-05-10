using System.Windows;
using System.Windows.Media;
using Blaze.Core.Extentions;
using MahApps.Metro.IconPacks;

namespace Blaze.Core
{
    public class Icon : IExtention<PackIconCodiconsKind, Icon>
    {
    }

    public class CornerRadiusExtention : IExtention<CornerRadius, CornerRadiusExtention>
    {
    }

    public class IsMouseOverColor : IExtention<Brush, IsMouseOverColor>
    {
    }

    public class IsPressedColor : IExtention<Brush, IsPressedColor>
    {
    }
}
