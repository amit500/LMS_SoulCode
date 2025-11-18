using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_SoulCode.Migrations
{
    /// <inheritdoc />
    public partial class userprogress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Categories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categories", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Courses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        Instructor = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DurationHours = table.Column<double>(type: "float", nullable: false),
            //        EnrolledCount = table.Column<int>(type: "int", nullable: false),
            //        Rating = table.Column<double>(type: "float", nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Lectures = table.Column<int>(type: "int", nullable: false),
            //        Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Courses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Permissions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Permissions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CourseVideos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CourseId = table.Column<int>(type: "int", nullable: false),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CourseVideos", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CourseVideos_Courses_CourseId",
            //            column: x => x.CourseId,
            //            principalTable: "Courses",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RolePermissions",
            //    columns: table => new
            //    {
            //        RoleId = table.Column<int>(type: "int", nullable: false),
            //        PermissionId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
            //        table.ForeignKey(
            //            name: "FK_RolePermissions_Permissions_PermissionId",
            //            column: x => x.PermissionId,
            //            principalTable: "Permissions",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RolePermissions_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserCourses",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        CourseId = table.Column<int>(type: "int", nullable: false),
            //        SubscribedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserCourses", x => new { x.UserId, x.CourseId });
            //        table.ForeignKey(
            //            name: "FK_UserCourses_Courses_CourseId",
            //            column: x => x.CourseId,
            //            principalTable: "Courses",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserCourses_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        RoleId = table.Column<int>(type: "int", nullable: false),
            //        RoleId1 = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_UserRoles_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserRoles_Roles_RoleId1",
            //            column: x => x.RoleId1,
            //            principalTable: "Roles",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserRoles_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "UserVideoProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    WatchedPercentage = table.Column<double>(type: "float", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    LastWatchedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideoProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVideoProgresses_CourseVideos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "CourseVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVideoProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_CourseVideos_CourseId",
            //    table: "CourseVideos",
            //    column: "CourseId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RolePermissions_PermissionId",
            //    table: "RolePermissions",
            //    column: "PermissionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserCourses_CourseId",
            //    table: "UserCourses",
            //    column: "CourseId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoles_RoleId",
            //    table: "UserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoles_RoleId1",
            //    table: "UserRoles",
            //    column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoProgresses_UserId",
                table: "UserVideoProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoProgresses_VideoId",
                table: "UserVideoProgresses",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Categories");

            //migrationBuilder.DropTable(
            //    name: "RolePermissions");

            //migrationBuilder.DropTable(
            //    name: "UserCourses");

            //migrationBuilder.DropTable(
            //    name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserVideoProgresses");

            //migrationBuilder.DropTable(
            //    name: "Permissions");

            //migrationBuilder.DropTable(
            //    name: "Roles");

            //migrationBuilder.DropTable(
            //    name: "CourseVideos");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "Courses");
        }
    }
}
