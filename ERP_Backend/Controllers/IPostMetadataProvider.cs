using Enterprise.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.API.Controllers;

public interface IMetadataProvider
{
    ActionResult<List<PostDTOMetaData>> GetPostDTOMetaData();
    ActionResult<List<string>> GetSortOptions();
}