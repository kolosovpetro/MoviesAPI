using System;
using System.Collections.Generic;
using CqrsApi.Models.Models;
using MediatR;

namespace CqrsApi.Queries.Queries
{
    public class GetByPredicateQuery : IRequest<IList<Movie>>
    {
        private readonly Func<bool, IList<Movie>> _predicate;

        public GetByPredicateQuery(Func<bool, IList<Movie>> predicate)
        {
            _predicate = predicate;
        }
    }
}