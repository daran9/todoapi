using System;
using Amazon.DynamoDBv2.DataModel;

namespace TodoApi.Infrastructure;

public abstract record Document
{
    [DynamoDBHashKey("pk")] public string Pk { get; init; }
    [DynamoDBRangeKey("sk")] public string Sk { get; init; }
    [DynamoDBProperty("created_date_utc")] public DateTime CreatedDateUtc { get; init; }

    [DynamoDBProperty("last_modified_date_utc")]
    public DateTime LastModifiedDateUtc { get; init; }
}