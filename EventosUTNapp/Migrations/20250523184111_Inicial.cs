using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventosUTNapp.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estadosinscripcion",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadosinscripcion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "metodospago",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metodospago", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "participantes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    telefono = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participantes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ponentes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    biografia = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ponentes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "salas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    ubicacion = table.Column<string>(type: "text", nullable: true),
                    capacidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipoeventos",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoeventos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lugar = table.Column<string>(type: "text", nullable: false),
                    tipoeventoid = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos", x => x.id);
                    table.ForeignKey(
                        name: "FK_eventos_tipoeventos_tipoeventoid",
                        column: x => x.tipoeventoid,
                        principalTable: "tipoeventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eventoponentes",
                columns: table => new
                {
                    eventoid = table.Column<int>(type: "integer", nullable: false),
                    ponenteid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventoponentes", x => new { x.eventoid, x.ponenteid });
                    table.ForeignKey(
                        name: "FK_eventoponentes_eventos_eventoid",
                        column: x => x.eventoid,
                        principalTable: "eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventoponentes_ponentes_ponenteid",
                        column: x => x.ponenteid,
                        principalTable: "ponentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inscripciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    participanteid = table.Column<int>(type: "integer", nullable: false),
                    eventoid = table.Column<int>(type: "integer", nullable: false),
                    fechainscripcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estadoid = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscripciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_inscripciones_estadosinscripcion_estadoid",
                        column: x => x.estadoid,
                        principalTable: "estadosinscripcion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inscripciones_eventos_eventoid",
                        column: x => x.eventoid,
                        principalTable: "eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inscripciones_participantes_participanteid",
                        column: x => x.participanteid,
                        principalTable: "participantes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sesiones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eventoid = table.Column<int>(type: "integer", nullable: false),
                    salaid = table.Column<int>(type: "integer", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    horainicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    horafin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sesiones", x => x.id);
                    table.ForeignKey(
                        name: "FK_sesiones_eventos_eventoid",
                        column: x => x.eventoid,
                        principalTable: "eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sesiones_salas_salaid",
                        column: x => x.salaid,
                        principalTable: "salas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "certificados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inscripcionid = table.Column<int>(type: "integer", nullable: false),
                    fechaemision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    urlcertificado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificados", x => x.id);
                    table.ForeignKey(
                        name: "FK_certificados_inscripciones_inscripcionid",
                        column: x => x.inscripcionid,
                        principalTable: "inscripciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inscripcionid = table.Column<int>(type: "integer", nullable: false),
                    metodopagoid = table.Column<short>(type: "smallint", nullable: false),
                    fechapago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    monto = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pagos_inscripciones_inscripcionid",
                        column: x => x.inscripcionid,
                        principalTable: "inscripciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pagos_metodospago_metodopagoid",
                        column: x => x.metodopagoid,
                        principalTable: "metodospago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_certificados_inscripcionid",
                table: "certificados",
                column: "inscripcionid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_eventoponentes_ponenteid",
                table: "eventoponentes",
                column: "ponenteid");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_tipoeventoid",
                table: "eventos",
                column: "tipoeventoid");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_estadoid",
                table: "inscripciones",
                column: "estadoid");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_eventoid",
                table: "inscripciones",
                column: "eventoid");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_participanteid",
                table: "inscripciones",
                column: "participanteid");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_inscripcionid",
                table: "pagos",
                column: "inscripcionid");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_metodopagoid",
                table: "pagos",
                column: "metodopagoid");

            migrationBuilder.CreateIndex(
                name: "IX_sesiones_eventoid",
                table: "sesiones",
                column: "eventoid");

            migrationBuilder.CreateIndex(
                name: "IX_sesiones_salaid",
                table: "sesiones",
                column: "salaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "certificados");

            migrationBuilder.DropTable(
                name: "eventoponentes");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "sesiones");

            migrationBuilder.DropTable(
                name: "ponentes");

            migrationBuilder.DropTable(
                name: "inscripciones");

            migrationBuilder.DropTable(
                name: "metodospago");

            migrationBuilder.DropTable(
                name: "salas");

            migrationBuilder.DropTable(
                name: "estadosinscripcion");

            migrationBuilder.DropTable(
                name: "eventos");

            migrationBuilder.DropTable(
                name: "participantes");

            migrationBuilder.DropTable(
                name: "tipoeventos");
        }
    }
}
