// ------------------------------------------------------------
// File: AsposePdfApi.GlobalUsings.g.cs
// ------------------------------------------------------------
// This file satisfies the missing generated global‑usings source file
// that the SDK expects.  It simply declares the namespaces that are
// required by the sample program as *global* usings so they are
// available throughout the compilation unit.

global using System;
global using System.IO;
global using System.Text;
global using Aspose.Pdf.Facades;

// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class UnicodeFormFieldHandler
{
    static void Main()
    {
        const string inputPdfPath  = "input_form.pdf";
        const string exportXfdfPath = "fields_utf8.xfdf";
        const string outputPdfPath = "output_form.pdf";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Create a Form facade bound to the source PDF
        using (Form form = new Form(inputPdfPath))
        {
            // ---------- Export form fields to XFDF with UTF‑8 encoding ----------
            // Export to a memory stream first
            using (MemoryStream exportStream = new MemoryStream())
            {
                form.ExportXfdf(exportStream);
                // Convert the exported bytes to a UTF‑8 string
                string xfdfContent = Encoding.UTF8.GetString(exportStream.ToArray());
                // Write the string to a file using UTF‑8 encoding explicitly
                File.WriteAllText(exportXfdfPath, xfdfContent, Encoding.UTF8);
            }

            // ---------- Import form fields from XFDF with UTF‑8 encoding ----------
            // Read the XFDF file as a UTF‑8 string
            string importedXfdf = File.ReadAllText(exportXfdfPath, Encoding.UTF8);
            // Convert the string back to a UTF‑8 byte array and load into a memory stream
            using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(importedXfdf)))
            {
                form.ImportXfdf(importStream);
            }

            // Save the modified PDF (fields now contain Unicode values)
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Exported XFDF to '{exportXfdfPath}' and saved updated PDF to '{outputPdfPath}'.");
    }
}
