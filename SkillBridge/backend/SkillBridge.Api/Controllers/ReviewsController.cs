using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillBridge.Api.DTOs.Reviews;
using SkillBridge.Api.Helpers;
using SkillBridge.Api.Services.Interfaces;

namespace SkillBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = RoleConstants.Student)]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<ActionResult<ReviewResponseDto>> AddReview(ReviewCreateDto request)
    {
        var review = await _reviewService.AddReviewAsync(User.GetUserId(), request);
        return Ok(review);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ReviewResponseDto>> UpdateReview(int id, ReviewUpdateDto request)
    {
        var review = await _reviewService.UpdateReviewAsync(id, User.GetUserId(), request);
        return review is null ? NotFound() : Ok(review);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var deleted = await _reviewService.DeleteReviewAsync(id, User.GetUserId());
        return deleted ? NoContent() : NotFound();
    }
}
