using System;
using Xunit;
using PatternMatcher;

namespace PatternMatcherTest
{
    public class SelectorTest {
		
		private bool var1;
		private int var2;
		private string var3;
		
		public SelectorTest() {
			this.var1 = true;
			this.var2 = 5;
			this.var3 = "hello";
		}
		
		[Fact]
		public void FirstOrTest() {
						
			int actual = Selector<int>.Match(var1, var2, var3)
				.With(true, 5, "hello").Return(10)
				.With(true, 3, "what's up").Return(20)
				.With(true, 5, "hello").Return(30)
				.FirstOr(-1);
				
			Assert.Equal(10, actual);
		}
		
		[Fact]
		public void LastOrTest() {
						
			int actual = Selector<int>.Match(var1, var2, var3)
				.With(true, 5, "hello").Return(10)
				.With(true, 3, "what's up").Return(20)
				.With(true, 5, "hello").Return(30)
				.LastOr(-1);
				
			Assert.Equal(30, actual);
		}

		[Fact]
		public void NotFoundTest() {
						
			int actual = Selector<int>.Match(var1, var2, var3)
				.With(true, 7, "hello").Return(10)
				.With(true, 3, "what's up").Return(20)
				.With(false, 5, "hello").Return(30)
				.LastOr(-1);
				
			Assert.Equal(-1, actual);
		}

		[Fact]
		public void StringResultTest() {
						
			string actual = Selector<string>.Match(var1, var2, var3)
				.With(false, 7, "hello").Return("one")
				.With(true, 3, "what's up").Return("two")
				.With(true, 5, "hello").Return("three")
				.LastOr("none");
				
			Assert.Equal("three", actual);
		}

		[Fact]
		public void FuncResultTest() {
						
			Func<int> actual = Selector<Func<int>>.Match(var1, var2, var3)
				.With(false, 7, "hello").Return(() => 1)
				.With(true, 3, "what's up").Return(() => 2)
				.With(true, 5, "hello").Return(() => 3)
				.FirstOr(() => -1);
				
			Assert.NotNull(actual);
			Assert.Equal(3, actual());
		}
	}
}