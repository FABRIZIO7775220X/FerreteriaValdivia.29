using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;



namespace FerreteriaValdivia.Models

{

    [Table("Caja")] // Asegura el nombre exacto de la tabla en MariaDB

    public class Caja

    {

        [Key]

        [Column("id_Caja")]

        public int id_Caja { get; set; }



        [Required]

        [DataType(DataType.Date)]

        [Column("Fecha_Apertura")]

        public DateTime Fecha_Apertura { get; set; }



        [Required]

        [Column("Monto_Inicial", TypeName = "decimal(18,2)")]

        public decimal Monto_Inicial { get; set; }



        [Column("Monto_Final", TypeName = "decimal(18,2)")]

        public decimal? Monto_Final { get; set; }



        [Column("id_Empleado")]

        public int id_Empleado { get; set; }



        [ForeignKey("id_Empleado")]

        public Empleado? Empleado { get; set; }

    }

}