namespace Fort
{
	public static class Extensions
	{
		public static void ThrowIfDefault<T>(this T? argument, String? name = null, String? message = null)
		{
			if (default(T)?.Equals(argument) ?? argument == null)
			{
				throw new ArgumentNullException(name, message);
			}
		}
		public static void ThrowIfNot<T>(this T argument, Func<T, Boolean> validation, String? message = null, String? name = null)
		{
			validation.ThrowIfDefault(nameof(validation));
			if (!validation.Invoke(argument))
			{
				throw new ArgumentException(message, name);
			}
		}
		public static void ThrowIfDefaultOrNot<T>(this T? argument, Func<T, Boolean> validation, String? message = null, String? name = null)
		{
			argument.ThrowIfDefault(name);
			argument.ThrowIfNot(validation!, message, name);
		}
		public static void ThrowIfDefaultOrEmpty<T>(this IEnumerable<T>? collection, String? name = null)
		{
			String? message = name != null ? $"{name} cannot be empty." : null;
			collection.ThrowIfDefaultOrNot(a => a.Any(), message, name);
		}
	}
}