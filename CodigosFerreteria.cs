namespace FerreteriaValdivia
{
    public static class CodigosFerreteria
    {
        // Normaliza cualquier entrada (ej: "1", "001", "prd-1" -> "PRD-0001")
        public static string NormalizarConPrefijo(string entrada, string prefijo, int digitos = 4, int idRespaldo = 0)
        {
            string limpio = (entrada ?? "").Trim().ToUpper();
            string prefijoConGuion = prefijo.Trim().ToUpper() + "-";

            // Si ya empieza con el prefijo (ej. "PRD-1" o "PRD-0001")
            if (limpio.StartsWith(prefijoConGuion))
            {
                string resto = limpio.Substring(prefijoConGuion.Length).Trim();
                if (int.TryParse(resto, out int numConPrefijo) && numConPrefijo > 0)
                {
                    return prefijoConGuion + numConPrefijo.ToString("D" + digitos);
                }
                return limpio;
            }

            // Si escribió solo un número (ej. "1" o "001")
            if (int.TryParse(limpio, out int numSolo) && numSolo > 0)
            {
                return prefijoConGuion + numSolo.ToString("D" + digitos);
            }

            // Si escribió otro texto no vacío
            if (!string.IsNullOrEmpty(limpio))
            {
                return prefijoConGuion + limpio;
            }

            // Respaldo si viene vacío y tenemos un ID numérico
            if (idRespaldo > 0)
            {
                return prefijoConGuion + idRespaldo.ToString("D" + digitos);
            }

            return prefijoConGuion + 1.ToString("D" + digitos);
        }

        // Extrae el número entero desde un código como "VTA-0001", "CLI-0002" o un número "1"
        public static bool IntentarObtenerNumero(string entrada, string prefijo, out int numero)
        {
            numero = 0;
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return false;
            }

            string limpio = entrada.Trim().ToUpper();
            string prefijoConGuion = (prefijo ?? "").Trim().ToUpper() + "-";
            string parteNumero = limpio;

            if (!string.IsNullOrWhiteSpace(prefijo) && limpio.StartsWith(prefijoConGuion))
            {
                parteNumero = limpio.Substring(prefijoConGuion.Length).Trim();
            }

            if (!int.TryParse(parteNumero, out int valor) || valor <= 0)
            {
                return false;
            }

            numero = valor;
            return true;
        }

        public static string Categoria(int idCategoria)
        {
            return "CAT-" + idCategoria.ToString("D2");
        }

        public static string Producto(string codigoProducto, int numeroRespaldo = 0)
        {
            return NormalizarConPrefijo(codigoProducto, "PRD", 4, numeroRespaldo);
        }

        public static string Cliente(int idCliente)
        {
            return "CLI-" + idCliente.ToString("D4");
        }

        public static string Proveedor(int idProveedor)
        {
            return "PRV-" + idProveedor.ToString("D4");
        }

        public static string Rol(int idRol)
        {
            return "ROL-" + idRol.ToString("D2");
        }

        // Genera CAJ-EMP-0001, CONT-EMP-0001 o EMP-0001 según el cargo o rol
        public static string Empleado(int idEmpleado, string cargoORol)
        {
            string texto = (cargoORol ?? "").Trim().ToLower();
            string numero = idEmpleado.ToString("D4");

            if (texto.Contains("cajero"))
            {
                return "CAJ-EMP-" + numero;
            }

            if (texto.Contains("contador"))
            {
                return "CONT-EMP-" + numero;
            }

            return "EMP-" + numero;
        }

        // Convierte un código ingresado (CAJ-EMP-0001, CONT-EMP-0001, EMP-0001 o número) en id_Empleado
        public static bool IntentarObtenerIdEmpleado(string entrada, out int idEmpleado)
        {
            idEmpleado = 0;

            if (string.IsNullOrWhiteSpace(entrada))
            {
                return false;
            }

            string limpio = entrada.Trim().ToUpper();

            if (limpio.StartsWith("CAJ-EMP-"))
            {
                return IntentarObtenerNumero(limpio, "CAJ-EMP", out idEmpleado);
            }

            if (limpio.StartsWith("CONT-EMP-"))
            {
                return IntentarObtenerNumero(limpio, "CONT-EMP", out idEmpleado);
            }

            if (limpio.StartsWith("EMP-"))
            {
                return IntentarObtenerNumero(limpio, "EMP", out idEmpleado);
            }

            return IntentarObtenerNumero(limpio, "", out idEmpleado);
        }

        public static string Caja(int idCaja)
        {
            return "CAJ-" + idCaja.ToString("D4");
        }

        public static string Venta(int idVenta)
        {
            return "VTA-" + idVenta.ToString("D4");
        }

        public static string DetalleVenta(int idDetalleVenta)
        {
            return "DET-" + idDetalleVenta.ToString("D4");
        }

        public static string Pago(int idPago)
        {
            return "PAG-" + idPago.ToString("D4");
        }
    }
}