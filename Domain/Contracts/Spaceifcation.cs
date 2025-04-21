using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public abstract class Spaceifcation<T> where T : class
	{
        protected Spaceifcation(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func< T, bool>> Criteria{get;}
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>> OrdrerBy { get; private set; }
		public Expression<Func<T, object>> OrdrerByDescending { get; private set; }

        public int Skip { get;  private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }
        protected void AddInclude(Expression<Func<T, object>> expression)
        
            =>Includes.Add(expression);



        protected void SetOrderBy(Expression<Func<T, object>> orderBy)
            => OrdrerBy = orderBy;

		protected void SetOrderByDescending(Expression<Func<T, object>> orderByDescending)
			=> OrdrerByDescending = orderByDescending;

        protected void ApplyPagination(int pageIndex,int pagsize)
        {
            IsPaginated = true;

            Take = pagsize;

            Skip=(pageIndex-1)*pagsize;
        }
	}
}
