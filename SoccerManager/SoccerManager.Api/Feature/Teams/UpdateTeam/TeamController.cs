﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoccerManager.Api.Feature.Teams.UpdateTeam
{
    [Route("api/team")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TeamController : ControllerBase
    {
        private readonly IUpdateTeamUseCase _updateTeamUseCase;
        public TeamController(IUpdateTeamUseCase updateTeamUseCase)
        {
            _updateTeamUseCase = updateTeamUseCase;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<UpdateTeamResponse> UpdateTeam(
                    [FromBody] UpdateTeamRequest request,
                    CancellationToken cancellationToken
                    )
        {
            var authorization = HttpContext.Request.Headers["Authorization"];

            var response = await _updateTeamUseCase.Handle(request, authorization, cancellationToken);
            return response.Value;
        }
    }
}
