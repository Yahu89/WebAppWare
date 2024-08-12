
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppWare.Utils
{
	public static class EnumExtensions
	{
		public static string? DisplayName(this Enum value)
		{
			if (value == null)
			{
				return null;
			}

			var field = value.GetType().GetField(value.ToString());
			var attributes = field?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

			return attributes?.Length > 0
				? attributes[0].Name
				: value.ToString();
		}

		public static IEnumerable<SelectListItem> ToSelectList<T>(
			Func<T, object> value,
			Func<T, string?> translate,
			Func<T, bool>? selected = null
		)
		{
            return Enum.GetValues(typeof(T))
				.Cast<T>()
				.Select(x => new SelectListItem
				{
					Value = value(x)?.ToString(),
					Text = translate(x)?.ToString(),
					Selected = selected == null ? false : selected(x),
				})
				.OrderBy(e => e.Text);
		}
    }
}
