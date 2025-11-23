using FluentValidation;
using LMS_SoulCode.Features.UserPermissions.Controllers;
using LMS_SoulCode.Features.UserPermissions.Models;
using LMS_SoulCode.Features.UserPermissions.DTOs;

namespace LMS_SoulCode.Features.UserPermissions.Validators
{
    public class RoleValidators
    {
        public class RoleRequestValidator : AbstractValidator<RoleRequest>
        {
            public RoleRequestValidator()
            {
                RuleFor(r => r.RoleName)
                    .NotEmpty().WithMessage("Role name is required")
                    .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters");

                RuleForEach(r => r.RolePermissions ?? new List<RolePermission>())
                    .SetValidator(new RolePermissionValidator());
            }
        }

        public class RolePermissionValidator : AbstractValidator<RolePermission>
        {
            public RolePermissionValidator()
            {
                RuleFor(rp => rp.RoleId)
                    .GreaterThan(0).WithMessage("RoleId must be greater than 0");

                RuleFor(rp => rp.PermissionId)
                    .GreaterThan(0).WithMessage("PermissionId must be greater than 0");
            }
        }

        public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
        {
            public CreateRoleRequestValidator()
            {
                RuleFor(x => x.RoleName)
                    .NotEmpty().WithMessage("Role name is required")
                    .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters");

                RuleForEach(x => x.PermissionIds)
                           .NotNull().WithMessage("Permission Id is required")
                           .GreaterThan(0).WithMessage("Permission Id must be greater than 0");
            }
        }

       
    }
}
