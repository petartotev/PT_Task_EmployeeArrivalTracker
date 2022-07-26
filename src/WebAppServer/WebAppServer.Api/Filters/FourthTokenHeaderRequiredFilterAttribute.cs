﻿using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using WebAppServer.Api.Exceptions;
using WebAppServer.Common;
using WebAppServer.V1.Contracts.Common;

namespace WebAppServer.Api.Filters;

public class FourthTokenHeaderRequiredFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var fourthTokenHeader = context?.HttpContext?.Request?.Headers?.SingleOrDefault(x => x.Key == Header.WebAppServer.FourthToken).Value;

        if (fourthTokenHeader is null || fourthTokenHeader.Value.ToArray().Length == 0)
        {
            throw new ApiException(HttpStatusCode.Forbidden, ErrorCode.BadRequest, $"{Header.WebAppServer.FourthToken} is a required header.");
        }

        Log.Information($"Request was received with [X-Fourth-Token]: {fourthTokenHeader.Value.ToArray().First()}");
    }
}
