using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE VIEW [dbo].[OrderView]
        AS
        SELECT
            O.ID,
            O.Address,
            O.DeliveryDate,
            O.Count,
            (P.Price * O.Count) AS TotalPrice,
            U.FirstName,
            U.LastName,
            U.UserName,
            U.PhoneNumber,
            U.Email
        FROM [Order] AS O
        JOIN [Product] AS P ON O.ProductID = P.ID
        JOIN [AspNetUsers] AS U ON O.UserID = U.ID
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[OrderView]");
        }
    }
}
