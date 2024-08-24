using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulerWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class on_delete_cascades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowRuns_Flows_FlowId",
                table: "FlowRuns");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_FlowTasks_FlowTaskId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_StartingUps_FlowTasks_PredecessorId",
                table: "StartingUps");

            migrationBuilder.DropForeignKey(
                name: "FK_StartingUps_FlowTasks_SuccessorId",
                table: "StartingUps");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRuns_Flows_FlowId",
                table: "FlowRuns",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_FlowTasks_FlowTaskId",
                table: "Flows",
                column: "FlowTaskId",
                principalTable: "FlowTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StartingUps_FlowTasks_PredecessorId",
                table: "StartingUps",
                column: "PredecessorId",
                principalTable: "FlowTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StartingUps_FlowTasks_SuccessorId",
                table: "StartingUps",
                column: "SuccessorId",
                principalTable: "FlowTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowRuns_Flows_FlowId",
                table: "FlowRuns");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_FlowTasks_FlowTaskId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_StartingUps_FlowTasks_PredecessorId",
                table: "StartingUps");

            migrationBuilder.DropForeignKey(
                name: "FK_StartingUps_FlowTasks_SuccessorId",
                table: "StartingUps");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRuns_Flows_FlowId",
                table: "FlowRuns",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_FlowTasks_FlowTaskId",
                table: "Flows",
                column: "FlowTaskId",
                principalTable: "FlowTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StartingUps_FlowTasks_PredecessorId",
                table: "StartingUps",
                column: "PredecessorId",
                principalTable: "FlowTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StartingUps_FlowTasks_SuccessorId",
                table: "StartingUps",
                column: "SuccessorId",
                principalTable: "FlowTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
