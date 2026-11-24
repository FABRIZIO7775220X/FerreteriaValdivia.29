using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace FerreteriaValdivia.Migrations
{
    /// <inheritdoc />
    public partial class InitialMariaDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    id_Categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre_categoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.id_Categoria);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id_Cliente);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    CodigoProducto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Nombre_Producto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false),
                    StockActual = table.Column<int>(type: "int", nullable: false),
                    CantidadVendida = table.Column<int>(type: "int", nullable: false),
                    CantidadVenta = table.Column<int>(type: "int", nullable: false),
                    id_Categoria = table.Column<int>(type: "int", nullable: true),
                    id_Proveedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.CodigoProducto);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    id_Proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Ubicacion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.id_Proveedor);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre_Rol = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.id_Rol);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    id_Empleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Cargo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    id_Rol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.id_Empleado);
                    table.ForeignKey(
                        name: "FK_Empleado_Rol_id_Rol",
                        column: x => x.id_Rol,
                        principalTable: "Rol",
                        principalColumn: "id_Rol");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre_Completo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Password_Hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Rolid_Rol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_Usuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_Rolid_Rol",
                        column: x => x.Rolid_Rol,
                        principalTable: "Rol",
                        principalColumn: "id_Rol");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Caja",
                columns: table => new
                {
                    id_Caja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Fecha_Apertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Monto_Inicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Monto_Final = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    id_Empleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.id_Caja);
                    table.ForeignKey(
                        name: "FK_Caja_Empleado_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleado",
                        principalColumn: "id_Empleado",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    id_Venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_Cliente = table.Column<int>(type: "int", nullable: false),
                    id_Empleado = table.Column<int>(type: "int", nullable: false),
                    id_Caja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.id_Venta);
                    table.ForeignKey(
                        name: "FK_Venta_Caja_id_Caja",
                        column: x => x.id_Caja,
                        principalTable: "Caja",
                        principalColumn: "id_Caja",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Cliente_id_Cliente",
                        column: x => x.id_Cliente,
                        principalTable: "Cliente",
                        principalColumn: "id_Cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Empleado_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleado",
                        principalColumn: "id_Empleado",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetalleVenta",
                columns: table => new
                {
                    id_DetalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_Venta = table.Column<int>(type: "int", nullable: false),
                    id_Producto = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio_Unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVenta", x => x.id_DetalleVenta);
                    table.ForeignKey(
                        name: "FK_DetalleVenta_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "CodigoProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVenta_Venta_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Venta",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    id_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "longtext", nullable: false),
                    Fecha = table.Column<string>(type: "longtext", nullable: false),
                    Hora = table.Column<string>(type: "longtext", nullable: false),
                    id_Venta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.id_Pago);
                    table.ForeignKey(
                        name: "FK_Pago_Venta_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Venta",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Caja_id_Empleado",
                table: "Caja",
                column: "id_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_id_Producto",
                table: "DetalleVenta",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_id_Venta",
                table: "DetalleVenta",
                column: "id_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_id_Rol",
                table: "Empleado",
                column: "id_Rol");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_id_Venta",
                table: "Pago",
                column: "id_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Rolid_Rol",
                table: "Usuario",
                column: "Rolid_Rol");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_id_Caja",
                table: "Venta",
                column: "id_Caja");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_id_Cliente",
                table: "Venta",
                column: "id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_id_Empleado",
                table: "Venta",
                column: "id_Empleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "DetalleVenta");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Caja");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
