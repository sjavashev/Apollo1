using Microsoft.AspNetCore.Mvc;
using MissionShared1.Tools;

namespace Houston1 {
	public static class ApiExtensions {
		public static IResult GetApiResult<T>(this OpResult<T> op) {
			if (op.Status == OpStatus.Forbidden) {
				return Results.StatusCode(403);
			}

			if (op.Status == OpStatus.AlreadyExists) {
				return Results.Conflict<string>(op.Description);
			}

			if (op.Status == OpStatus.NotFound) {
				return Results.NotFound<string>(op.Description);
			}

			if (op.Status != OpStatus.OK) {
				return Results.BadRequest<string>(op.Description);
			}

			return Results.Ok(op.Data);
		}
	}
}
