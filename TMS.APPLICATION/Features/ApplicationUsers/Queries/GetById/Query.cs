using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TMS.APPLICATION.Common.Responses;

namespace TMS.APPLICATION.Features.ApplicationUsers.Queries.GetById
{
    public class Query : IRequest<Response>
    {
        public required string Id { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}