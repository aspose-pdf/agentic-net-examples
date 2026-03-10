using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace MhtConversionWithPageEditing
{
    /// <summary>
    /// Provides methods to load an MHT file, edit its pages using PdfPageEditor,
    /// and save the result back to a web‑friendly format.
    /// </summary>
    public static class MhtPageEditor
    {
        /// <summary>
        /// Loads an MHT document, applies page‑level transformations, and saves the edited file.
        /// </summary>
        /// <param name="inputMhtPath">Full path to the source .mht file.</param>
        /// <param name="outputPath">Full path where the edited file will be saved. The extension can be .html (fallback when MHT is not supported).</param>
        public static void ConvertAndEdit(string inputMhtPath, string outputPath)
        {
            if (!File.Exists(inputMhtPath))
                throw new FileNotFoundException("Input MHT file not found.", inputMhtPath);

            // Load the MHT file into a PDF Document using the dedicated load options.
            MhtLoadOptions loadOptions = new MhtLoadOptions();
            using (Document pdfDoc = new Document(inputMhtPath, loadOptions))
            {
                // Create a PdfPageEditor facade to manipulate page properties.
                using (PdfPageEditor pageEditor = new PdfPageEditor())
                {
                    // Bind the loaded PDF document to the editor.
                    pageEditor.BindPdf(pdfDoc);

                    // -----------------------------------------------------------------
                    // Example 1: Rotate the first page 90 degrees clockwise.
                    // -----------------------------------------------------------------
                    pageEditor.Rotation = 90;                     // rotation in degrees (0,90,180,270)
                    pageEditor.ProcessPages = new int[] { 1 };    // affect only page 1
                    pageEditor.ApplyChanges();                    // commit changes

                    // -----------------------------------------------------------------
                    // Example 2: Zoom the second page to 150% (1.5x).
                    // -----------------------------------------------------------------
                    pageEditor.Rotation = 0;                      // reset rotation
                    pageEditor.Zoom = 1.5f;                       // zoom factor (1.0 = 100%)
                    pageEditor.ProcessPages = new int[] { 2 };
                    pageEditor.ApplyChanges();

                    // -----------------------------------------------------------------
                    // Example 3: Move the origin of the third page (shift content).
                    // -----------------------------------------------------------------
                    pageEditor.Zoom = 1.0f;                       // reset zoom
                    pageEditor.MovePosition(50f, 100f);           // shift origin (points)
                    pageEditor.ProcessPages = new int[] { 3 };
                    pageEditor.ApplyChanges();

                    // -----------------------------------------------------------------
                    // Example 4: Change the page size of all pages to A4.
                    // -----------------------------------------------------------------
                    pageEditor.PageSize = PageSize.A4;            // set target page size
                    pageEditor.ProcessPages = null;               // null = all pages
                    pageEditor.ApplyChanges();
                }

                // After all edits, save the document back to a web‑friendly format.
                // The older Aspose.Pdf versions do not expose SaveFormat.MHT; we fall back to HTML.
                // If a newer version is referenced, replace SaveFormat.Html with SaveFormat.Mhtml.
                pdfDoc.Save(outputPath, SaveFormat.Html);
            }
        }
    }

    // ---------------------------------------------------------------------
    // Simple console entry point – required for a self‑contained executable.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Basic argument validation.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: MhtConversionWithPageEditing <input.mht> <output.html>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                MhtPageEditor.ConvertAndEdit(inputPath, outputPath);
                Console.WriteLine($"Edited document saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
