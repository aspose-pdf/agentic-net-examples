using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAnnotationDeletionTool
{
    class Program
    {
        // Path to the source PDF (adjust as needed)
        private static readonly string SourcePdfPath = "sample.pdf";
        // Temporary file used for preview after deletion
        private static readonly string PreviewPdfPath = Path.Combine(Path.GetTempPath(), "preview.pdf");

        // List of annotation type names that can be deleted (matches Aspose.Pdf.AnnotationType enum values)
        private static readonly string[] AvailableAnnotationTypes = new string[]
        {
            "Text", "Link", "Highlight", "Underline", "StrikeOut", "Squiggly",
            "FreeText", "Line", "Square", "Circle", "Polygon", "PolyLine",
            "Stamp", "Caret", "FileAttachment", "Sound", "Movie", "Screen",
            "RichMedia", "Popup", "Watermark"
        };

        static void Main()
        {
            Console.Title = "PDF Annotation Deletion Tool";
            if (!File.Exists(SourcePdfPath))
            {
                Console.WriteLine($"Source PDF not found: {SourcePdfPath}");
                return;
            }

            // Show the list of deletable annotation types
            Console.WriteLine("Select annotation types to delete (comma‑separated numbers):");
            for (int i = 0; i < AvailableAnnotationTypes.Length; i++)
            {
                Console.WriteLine($"  {i + 1,2}. {AvailableAnnotationTypes[i]}");
            }
            Console.Write("Your choice: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No selection made – exiting.");
                return;
            }

            // Parse user selection
            var selectedTypes = ParseSelection(input);
            if (selectedTypes.Count == 0)
            {
                Console.WriteLine("No valid annotation types selected – exiting.");
                return;
            }

            // Copy original PDF to a temporary file for preview
            File.Copy(SourcePdfPath, PreviewPdfPath, true);

            // Delete the selected annotation types using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(PreviewPdfPath);
                foreach (string annotType in selectedTypes)
                {
                    // Delete all annotations of the specified type
                    editor.DeleteAnnotations(annotType);
                }
                editor.Save(PreviewPdfPath);
                editor.Close();
            }

            // Open the preview PDF with the default system viewer
            try
            {
                Process.Start(new ProcessStartInfo(PreviewPdfPath) { UseShellExecute = true });
                Console.WriteLine("Preview opened. Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to open preview PDF: {ex.Message}");
            }
        }

        /// <summary>
        /// Parses a comma‑separated list of numbers entered by the user and returns the corresponding annotation type names.
        /// </summary>
        private static List<string> ParseSelection(string input)
        {
            var result = new List<string>();
            var parts = input.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int index))
                {
                    // Convert to zero‑based index
                    index -= 1;
                    if (index >= 0 && index < AvailableAnnotationTypes.Length)
                    {
                        string typeName = AvailableAnnotationTypes[index];
                        if (!result.Contains(typeName))
                            result.Add(typeName);
                    }
                }
            }
            return result;
        }
    }
}
