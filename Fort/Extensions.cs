using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fort
{
	public static class Extensions
	{
		/// <summary>
		/// Throws <see cref="ArgumentException"/> when <paramref name="argument"/> does not match against <paramref name="validation"/>.
		/// </summary>
		/// <typeparam name="T">The argument Type.</typeparam>
		/// <param name="argument">The argument to check.</param>
		/// <param name="validation">The predicate against which <paramref name="argument"/> must match.</param>
		/// <param name="name">The name of <paramref name="argument"/>.</param>
		/// <param name="message">The exception message.</param>
		/// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not match against <paramref name="validation"/>.</exception>
		public static void ThrowIfNot<T>(this T argument, Func<T, Boolean> validation, String message = null, String name = null)
		{
			validation.ThrowIfDefault(nameof(validation));
			if (!validation.Invoke(argument))
			{
				throw new ArgumentException(message, name);
			}
		}

		#region Null
		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="argument"/> is <see langword="null"/>.
		/// </summary>
		/// <typeparam name="T">The argument Type.</typeparam>
		/// <param name="argument">The argument to check.</param>
		/// <param name="name">The name of <paramref name="argument"/>.</param>
		/// <param name="message">The exception message.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is <see langword="null"/>.</exception>
		public static void ThrowIfNull<T>(this T argument, String name = null, String message = null)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name, message);
			}
		}

		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="argument"/> is <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="argument"/> does not match against <paramref name="validation"/>.
		///  </summary>
		/// <typeparam name="T">The argument Type.</typeparam>
		/// <param name="argument">The argument to check.</param>
		/// <param name="validation">The predicate against which <paramref name="argument"/> must match.</param>
		/// <param name="name">The name of <paramref name="argument"/>.</param>
		/// <param name="message">The exception message.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not match against <paramref name="validation"/>.</exception>
		public static void ThrowIfNullOrNot<T>(this T argument, Func<T, Boolean> validation, String message = null, String name = null)
		{
			argument.ThrowIfNull(name);
			argument.ThrowIfNot(validation, message, name);
		}

		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="enumeration"/> is <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="collection"/> is empty.
		/// </summary>
		/// <typeparam name="T">The item type of <paramref name="enumeration"/>.</typeparam>
		/// <param name="enumeration">The enumeration to check.</param>
		/// <param name="name">The name of <paramref name="enumeration"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="enumeration"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="enumeration"/> is empty.</exception>
		public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> enumeration, String name = null)
		{
			if (enumeration is T[] array)
			{
				array.ThrowIfNullOrEmpty(name);
			}
			else if (enumeration is ICollection collection)
			{
				collection.ThrowIfNullOrEmpty(name);
			}
			else
			{
				String message = GetEmptyCollectionMessage(name ?? "Enumeration");
				enumeration.ThrowIfNullOrNot(a => a.GetEnumerator().MoveNext(), message, name);
			}
		}
		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="array"/> is <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="collection"/> is empty.
		/// </summary>
		/// <typeparam name="T">The item type of <paramref name="array"/>.</typeparam>
		/// <param name="array">The array to check.</param>
		/// <param name="name">The name of <paramref name="array"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="array"/> is empty.</exception>
		public static void ThrowIfNullOrEmpty<T>(this T[] array, String name = null)
		{
			String message = GetEmptyCollectionMessage(name ?? "Array");
			array.ThrowIfNullOrNot(a => a.Length > 0, message, name);
		}
		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="collection"/> is <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="collection"/> is empty.
		/// </summary>
		/// <typeparam name="T">The item type of <paramref name="collection"/>.</typeparam>
		/// <param name="collection">The collection to check.</param>
		/// <param name="name">The name of <paramref name="collection"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> is empty.</exception>
		public static void ThrowIfNullOrEmpty(this ICollection collection, String name = null)
		{
			String message = GetEmptyCollectionMessage(name ?? "Collection");
			collection.ThrowIfNullOrNot(a => a.Count > 0, message, name);
		}
		#endregion
		#region Default
		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="argument"/> is <see langword="default"/> or <see langword="null"/>.
		/// </summary>
		/// <typeparam name="T">The argument Type.</typeparam>
		/// <param name="argument">The argument to check.</param>
		/// <param name="name">The name of <paramref name="argument"/>.</param>
		/// <param name="message">The exception message.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is <see langword="default"/> or <see langword="null"/>.</exception>
		public static void ThrowIfDefault<T>(this T argument, String name = null, String message = null)
		{
			if (default(T)?.Equals(argument) ?? argument == null)
			{
				throw new ArgumentNullException(name, message);
			}
		}

		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="argument"/> is <see langword="default"/> or <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="argument"/> does not match against <paramref name="validation"/>.
		///  </summary>
		/// <typeparam name="T">The argument Type.</typeparam>
		/// <param name="argument">The argument to check.</param>
		/// <param name="validation">The predicate against which <paramref name="argument"/> must match.</param>
		/// <param name="name">The name of <paramref name="argument"/>.</param>
		/// <param name="message">The exception message.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is <see langword="default"/> or <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not match against <paramref name="validation"/>.</exception>
		public static void ThrowIfDefaultOrNot<T>(this T argument, Func<T, Boolean> validation, String message = null, String name = null)
		{
			argument.ThrowIfDefault(name);
			argument.ThrowIfNot(validation, message, name);
		}

		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="enumeration"/> is <see langword="default"/> or <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="collection"/> is empty.
		/// </summary>
		/// <typeparam name="T">The item type of <paramref name="enumeration"/>.</typeparam>
		/// <param name="enumeration">The enumeration to check.</param>
		/// <param name="name">The name of <paramref name="enumeration"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="enumeration"/> is <see langword="default"/> or <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="enumeration"/> is empty.</exception>
		public static void ThrowIfDefaultOrEmpty<T>(this IEnumerable<T> enumeration, String name = null)
		{
			if (enumeration is T[] array)
			{
				array.ThrowIfDefaultOrEmpty(name);
			}
			else if (enumeration is ICollection collection)
			{
				collection.ThrowIfDefaultOrEmpty(name);
			}
			else
			{
				String message = GetEmptyCollectionMessage(name ?? "Enumeration");
				enumeration.ThrowIfDefaultOrNot(a => a.GetEnumerator().MoveNext(), message, name);
			}
		}
		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="array"/> is <see langword="default"/> or <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="collection"/> is empty.
		/// </summary>
		/// <typeparam name="T">The item type of <paramref name="array"/>.</typeparam>
		/// <param name="array">The array to check.</param>
		/// <param name="name">The name of <paramref name="array"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> is <see langword="default"/> or <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="array"/> is empty.</exception>
		public static void ThrowIfDefaultOrEmpty<T>(this T[] array, String name = null)
		{
			String message = GetEmptyCollectionMessage(name ?? "Array");
			array.ThrowIfDefaultOrNot(a => a.Length > 0, message, name);
		}
		/// <summary>
		/// Throws <see cref="ArgumentNullException"/> when <paramref name="collection"/> is <see langword="default"/> or <see langword="null"/> or an <see cref="ArgumentException"/> when <paramref name="collection"/> is empty.
		/// </summary>
		/// <typeparam name="T">The item type of <paramref name="collection"/>.</typeparam>
		/// <param name="collection">The collection to check.</param>
		/// <param name="name">The name of <paramref name="collection"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <see langword="default"/> or <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> is empty.</exception>
		public static void ThrowIfDefaultOrEmpty(this ICollection collection, String name = null)
		{
			String message = GetEmptyCollectionMessage(name ?? "Collection");
			collection.ThrowIfDefaultOrNot(a => a.Count > 0, message, name);
		}
		#endregion

		private static String GetEmptyCollectionMessage(String name)
		{
			return name != null ? $"{name} cannot be empty." : null;
		}
	}
}