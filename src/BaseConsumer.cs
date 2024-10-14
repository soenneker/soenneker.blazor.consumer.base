using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.ApiClient.Abstract;
using Soenneker.Blazor.ApiClient.Dtos;
using Soenneker.Blazor.Consumer.Base.Abstract;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Extensions.HttpResponseMessage;
using Soenneker.Extensions.Object;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Blazor.Consumer.Base;

///<inheritdoc cref="IBaseConsumer"/>
public class BaseConsumer : IBaseConsumer
{
    protected readonly IApiClient ApiClient;
    protected readonly ILogger<BaseConsumer> Logger;

    protected readonly string PrefixUri;

    protected readonly bool LogRequest = false;
    protected readonly bool LogResponse = false;

    protected BaseConsumer(IApiClient apiClient, ILogger<BaseConsumer> logger, string prefixUri)
    {
        ApiClient = apiClient;
        Logger = logger;
        PrefixUri = prefixUri;
    }

    [Pure]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Get<TResponse>(string id, string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var options = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Get(options, cancellationToken: cancellationToken).NoSync();
        (TResponse?, ProblemDetailsDto?) response = await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async Task<(TResponse? response, ProblemDetailsDto? details)> GetTask<TResponse>(string id, string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var options = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Get(options, cancellationToken: cancellationToken).NoSync();
        (TResponse?, ProblemDetailsDto?) response = await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async ValueTask<(List<TResponse>? response, ProblemDetailsDto? details)> GetAll<TResponse>(string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;
        var options = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Get(options, cancellationToken: cancellationToken).NoSync();
        (List<TResponse>?, ProblemDetailsDto?) response = await message.ToWithDetails<List<TResponse>>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async Task<(List<TResponse>? response, ProblemDetailsDto? details)> GetAllTask<TResponse>(string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;
        var options = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Get(options, cancellationToken: cancellationToken).NoSync();
        (List<TResponse>?, ProblemDetailsDto?) response = await message.ToWithDetails<List<TResponse>>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Create<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? PrefixUri;
        var options = new RequestOptions { Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Post(options, cancellationToken).NoSync();

        (TResponse?, ProblemDetailsDto?) response = await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Update<TResponse>(string id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var options = new RequestOptions { Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Put(options, cancellationToken).NoSync();

        (TResponse?, ProblemDetailsDto?) response = await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Delete<TResponse>(string id, string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var options = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Delete(options, cancellationToken).NoSync();

        (TResponse?, ProblemDetailsDto?) response = await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
        return response;
    }

    [Pure]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Upload<TResponse>(Stream stream, string fileName, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;
        var options = new RequestUploadOptions { Uri = uri, Stream = stream, FileName = fileName, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        HttpResponseMessage message = await ApiClient.Upload(options, cancellationToken).NoSync();
        (TResponse?, ProblemDetailsDto?) response = await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
        return response;
    }
}