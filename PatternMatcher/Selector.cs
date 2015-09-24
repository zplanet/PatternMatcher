using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternMatcher {
	
	public class Selector<TResult> {
		
		public static Selector<TResult> Match(params object[] objs) {
			return new Selector<TResult>() { baseLine = objs };
		}
		
		private Selector() {}
		private Object[] baseLine;
		private List<Case> cases = new List<Case>();
		
		public Case With(params object[] objs) {
			var c = new Case(this, objs);
			this.cases.Add(c);
			return c;
		}
		
		private bool CompareBaseLineWithCase(Case c) {
			return this.baseLine.SequenceEqual(c.Objects);
		}
		
		public TResult FirstOr(TResult result) {
			Case c = this.cases.FirstOrDefault(CompareBaseLineWithCase);
			return (null == c) ? result : c.Result;
		}
		
		public TResult LastOr(TResult result) {
			Case c = this.cases.LastOrDefault(CompareBaseLineWithCase);
			return (null == c) ? result : c.Result;
		}
		
		public IEnumerable<TResult> FindAll() {
			return this.cases
					.FindAll(CompareBaseLineWithCase)
					.Select(c => c.Result);
		}
				
		public class Case {
			
			private Case() {}
			
			internal Case(Selector<TResult> selector, params object[] objs) {
				this.selector = selector;
				this.Objects = objs;
			}
			
			private Selector<TResult> selector;
			internal TResult Result { get; set; }
			
			internal object[] Objects { get; set; }
			
			public Selector<TResult> Return(TResult result) {
				this.Result = result;
				return this.selector;
			}
 		}
	}
}