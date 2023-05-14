using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionShared1.Tools {
	public enum OpStatus {
		OK,
		Fail,
		Forbidden,
		AlreadyExists,
		NotFound,
	}

	public class OpResult<T> {
		public T? Data { get; }
		public OpStatus Status { get; }
		public string? Description { get; }

		public OpResult(T? data, OpStatus status = OpStatus.OK, string? description = null) {
			this.Data = data;
			this.Status = status;
			this.Description = description;
		}

		public static OpResult<T> Fail(OpStatus status = OpStatus.Fail, string? description = null)
			=> new OpResult<T>(default, status, description);
	}
}
