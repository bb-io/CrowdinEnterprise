﻿using Apps.CrowdinEnterprise.Webhooks.Models.Payload.File;
using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.SourceFiles;
using File = Crowdin.Api.SourceFiles.File;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class FileEntity
{
    [Display("ID")] public string Id { get; set; }
    [Display("Project ID")] public string ProjectId { get; set; }
    [Display("Branch ID")] public string? BranchId { get; set; }
    [Display("Directory ID")] public string? DirectoryId { get; set; }
    public string Name { get; set; }
    public string? Title { get; set; }
    public string Type { get; set; }
    public string Path { get; set; }
    public string Status { get; set; }
    [Display("Created at")] public DateTime? CreatedAt { get; set; }

    public FileEntity(FileCollectionResource file)
    {
        Id = file.Id.ToString();
        ProjectId = file.ProjectId.ToString();
        BranchId = file.BranchId.ToString();
        DirectoryId = file.DirectoryId.ToString();
        Name = file.Name;
        Title = file.Title;
        Type = file.Type;
        Path = file.Path;
        Status = file.Status.ToString();
        CreatedAt = file.CreatedAt.DateTime;
    }

    public FileEntity(File file)
    {
        Id = file.Id.ToString();
        ProjectId = file.ProjectId.ToString();
        BranchId = file.BranchId?.ToString();
        DirectoryId = file.DirectoryId.ToString();
        Name = file.Name;
        Title = file.Title;
        Type = file.Type;
        Path = file.Path;
        Status = file.Status.ToString();
        CreatedAt = file.CreatedAt.DateTime;
    }

    public FileEntity(FileResource file)
    {
        Id = file.Id.ToString();
        ProjectId = file.ProjectId.ToString();
        BranchId = file.BranchId.ToString();
        DirectoryId = file.DirectoryId.ToString();
        Name = file.Name;
        Title = file.Title;
        Type = file.Type;
        Path = file.Path;
        Status = file.Status.ToString();
        CreatedAt = file.CreatedAt.DateTime;
    }

    public FileEntity(FilePayloadWithProject file)
    {
        Id = file.Id.ToString();
        ProjectId = file.Project.Id.ToString();
        BranchId = file.BranchId;
        DirectoryId = file.DirectoryId;
        Name = file.Name;
        Title = file.Title;
        Type = file.Type;
        Path = file.Path;
        Status = file.Status.ToString();
    }
}