using Fort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fort.Tests
{
	[TestClass]
	public class ExtensionTests
	{
		#region Data
		private static Object[][] Defaults
		{
			get
			{
				return new Object[][]
				{
					new object[]
					{
						null, "Object Value", "value cannot be null"
					},new object[]
					{
						default(Byte), "Byte Value", "Sample message"
					},new object[]
					{
						default(SByte), "Signed Byte Value", String.Empty
					},new object[]
					{
						default(String),"String Value", "Expected non null value"
					},new object[]
					{
						default(Int16), "Integer 16 Value", "..."
					},new object[]
					{
						default(UInt16), "Unsigned Integer 16 Value","-"
					},new object[]
					{
						default(Int32), "Integer 32 Value", "integer expected"
					},new object[]
					{
						default(UInt32), "Unsigned Integer 34 Value", null
					},new object[]
					{
						default(Int64), null, "Some message"
					},new object[]
					{
						default(UInt64), null, null
					},new object[]
					{
						default(Single), "Single Value", "none"
					},new object[]
					{
						default(Double), "Double value", "11312"
					},new object[]
					{
						default(Decimal), "Decimal Value", String.Empty
					},new object[]
					{
						default(Object), "Object Value", null
					}
				};
			}
		}
		private static Object[][] NonDefaults
		{
			get
			{
				return new Object[][]
				{
					new object[]
					{
						"Some Value", "Object Value", "value cannot be null"
					},new object[]
					{
						Byte.MaxValue, "Byte Value", "Sample message"
					},new object[]
					{
						SByte.MinValue, "Signed Byte Value", String.Empty
					},new object[]
					{
						String.Empty,"String Value", "Expected non null value"
					},new object[]
					{
						Int16.MinValue, "Integer 16 Value", "..."
					},new object[]
					{
						UInt16.MaxValue, "Unsigned Integer 16 Value","-"
					},new object[]
					{
						Int32.MinValue, "Integer 32 Value", "integer expected"
					},new object[]
					{
						UInt32.MaxValue, "Unsigned Integer 34 Value", null
					},new object[]
					{
						Int64.MinValue, null, "Some message"
					},new object[]
					{
						UInt64.MinValue, null, null
					},new object[]
					{
						Single.MinValue, "Single Value", "none"
					},new object[]
					{
						Double.MaxValue, "Double value", "11312"
					},new object[]
					{
						Decimal.MinValue, "Decimal Value", String.Empty
					},new object[]
					{
						new Object(), "Object Value", null
					}
				};
			}
		}
		private static Object[][] Nulls
		{
			get
			{
				return new Object[][]
				{
					new object[]
					{
						null, "Object Value", "value cannot be null"
					},new object[]
					{
						default(String),"String Value", "Expected non null value"
					},new object[]
					{
						default(Object), "Object Value", null
					}
				};
			}
		}
		private static Object[][] NonNulls
		{
			get
			{
				return new Object[][]
				{
					new object[]
					{
						"Some Value", "Object Value", "value cannot be null"
					},new object[]
					{
						Byte.MaxValue, "Byte Value", "Sample message"
					},new object[]
					{
						SByte.MinValue, "Signed Byte Value", String.Empty
					},new object[]
					{
						String.Empty,"String Value", "Expected non null value"
					},new object[]
					{
						Int16.MinValue, "Integer 16 Value", "..."
					},new object[]
					{
						UInt16.MaxValue, "Unsigned Integer 16 Value","-"
					},new object[]
					{
						Int32.MinValue, "Integer 32 Value", "integer expected"
					},new object[]
					{
						UInt32.MaxValue, "Unsigned Integer 34 Value", null
					},new object[]
					{
						Int64.MinValue, null, "Some message"
					},new object[]
					{
						UInt64.MinValue, null, null
					},new object[]
					{
						Single.MinValue, "Single Value", "none"
					},new object[]
					{
						Double.MaxValue, "Double value", "11312"
					},new object[]
					{
						Decimal.MinValue, "Decimal Value", String.Empty
					},new object[]
					{
						new Object(), "Object Value", null
					},new object[]
					{
						default(Byte), "Byte Value", "Sample message"
					},new object[]
					{
						default(SByte), "Signed Byte Value", String.Empty
					},new object[]
					{
						default(Int16), "Integer 16 Value", "..."
					},new object[]
					{
						default(UInt16), "Unsigned Integer 16 Value","-"
					},new object[]
					{
						default(Int32), "Integer 32 Value", "integer expected"
					},new object[]
					{
						default(UInt32), "Unsigned Integer 34 Value", null
					},new object[]
					{
						default(Int64), null, "Some message"
					},new object[]
					{
						default(UInt64), null, null
					},new object[]
					{
						default(Single), "Single Value", "none"
					},new object[]
					{
						default(Double), "Double value", "11312"
					},new object[]
					{
						default(Decimal), "Decimal Value", String.Empty
					}
				};
			}
		}
		#endregion
		#region ThrowIfDefault
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void Default(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfDefault(), ex => String.IsNullOrEmpty(ex.ParamName));
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void NamedDefault(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfDefault(name), ex => ex.ParamName == name);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void MessageDefault(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfDefault(message: message), ex => String.IsNullOrEmpty(ex.ParamName) && (message == null || ex.Message.Substring(0, message.Length) == message));
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void NamedMessageDefault(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfDefault(name, message),
				ex =>
				{
					var result = ex.ParamName == name && (message == null || ex.Message.Substring(0, message.Length) == message);
					return result;
				});
		}
		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NonDefault(Object value, String name, String message)
		{
			value.ThrowIfDefault();
		}
		#endregion
		#region ThrowIfNot
		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NonDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void DefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NonDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void DefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true);
		}

		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NamedNonDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false, name: name),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void NamedDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false, name: name),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NamedNonDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true, name: name);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void NamedDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true, name: name);
		}

		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void MessageNonDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false, message),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void MessageDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false, message),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void MessageNonDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true, message);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void MessageDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true, message);
		}

		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NamedMessageNonDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false, message, name),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void NamedMessageDefaultIfNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentException>(
				() => value.ThrowIfNot(o => false, message, name),
				ex => true);
		}
		[TestMethod]
		[DynamicData(nameof(NonDefaults))]
		public void NamedMessageNonDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true, message, name);
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void NamedMessageDefaultNotIfNot(Object value, String name, String message)
		{
			value.ThrowIfNot(o => true, message, name);
		}
		#endregion
		#region ThrowIfDefaultOrNot
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void DefaultTrueThrowIfDefaultOrNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfDefaultOrNot(o => true), ex => String.IsNullOrEmpty(ex.ParamName));
		}
		[TestMethod]
		[DynamicData(nameof(Defaults))]
		public void DefaultFalseThrowIfDefaultOrNot(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfDefaultOrNot(o => false), ex => String.IsNullOrEmpty(ex.ParamName));
		}
		#endregion

		#region ThrowIfNull
		[TestMethod]
		[DynamicData(nameof(Nulls))]
		public void Null(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfNull(), ex => String.IsNullOrEmpty(ex.ParamName));
		}
		[TestMethod]
		[DynamicData(nameof(Nulls))]
		public void NamedNull(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfNull(name), ex => ex.ParamName == name);
		}
		[TestMethod]
		[DynamicData(nameof(Nulls))]
		public void MessageNull(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfNull(message: message), ex => String.IsNullOrEmpty(ex.ParamName) && (message == null || ex.Message.Substring(0, message.Length) == message));
		}
		[TestMethod]
		[DynamicData(nameof(Nulls))]
		public void NamedMessageNull(Object value, String name, String message)
		{
			AssertThrows<ArgumentNullException>(() => value.ThrowIfNull(name, message),
				ex =>
				{
					var result = ex.ParamName == name && (message == null || ex.Message.Substring(0, message.Length) == message);
					return result;
				});
		}
		[TestMethod]
		[DynamicData(nameof(NonNulls))]
		public void NonNull(Object value, String name, String message)
		{
			value.ThrowIfNull();
		}
		#endregion
		//TODO: more tests eh

		private static void AssertThrows<TException>(Action action, Func<TException, Boolean> matchPredicate)
			where TException : Exception
		{
			try
			{
				action.Invoke();
				return;
			}
			catch (TException ex)
			when (matchPredicate(ex))
			{
				return;
			}
			catch (Exception ex)
			{
				throw new AssertFailedException("Incorrect exception thrown.", ex);
			}
			throw new AssertFailedException($"Expected exception of type '{typeof(TException)}'.");
		}
	}
}