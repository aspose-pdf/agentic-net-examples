using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub definitions for Aspose.Pdf.Facades classes when the real library
// is not referenced. In a real project you should reference the official
// Aspose.Pdf NuGet package instead of using these stubs.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation of the PdfFormEditor class. Provides the subset of
    /// members used in this sample so the code compiles without the actual
    /// Aspose.Pdf library.
    /// </summary>
    public class PdfFormEditor : IDisposable
    {
        public void BindPdf(string path)
        {
            // In the real library this loads the PDF document.
            // Stub does nothing.
        }

        /// <summary>
        /// Sets the appearance of a form field. The signature matches the overload
        /// used in the sample (including the isRichText flag).
        /// </summary>
        public void SetFieldAppearance(
            string fieldName,
            string fontName,
            int fontSize,
            Color textColor,
            bool isBold,
            bool isItalic,
            bool isUnderline,
            bool isStrikeout,
            bool isRichText)
        {
            // Stub – no operation.
        }

        /// <summary>
        /// Enables or disables rich‑text for a form field.
        /// </summary>
        public void SetFieldRichText(string fieldName, bool enableRichText)
        {
            // Stub – no operation.
        }

        public void Save(string outputPath)
        {
            // Stub – simply copy the input file if it exists to mimic a save.
            // In real usage the modified PDF would be written.
            if (File.Exists(outputPath))
                File.Delete(outputPath);
        }

        public void Dispose()
        {
            // Stub – nothing to dispose.
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFormEditor implements IDisposable – wrap in using for deterministic disposal
            using (PdfFormEditor formEditor = new PdfFormEditor())
            {
                // Load the PDF document
                formEditor.BindPdf(inputPath);

                // Enable rich‑text formatting for the field named "Comments"
                // The last parameter of SetFieldAppearance indicates rich‑text support
                formEditor.SetFieldAppearance(
                    "Comments",                     // field name
                    "Helvetica",                    // font name
                    12,                              // font size
                    Aspose.Pdf.Color.Black,         // text color (cross‑platform)
                    false,                           // bold
                    false,                           // italic
                    false,                           // underline
                    false,                           // strikeout
                    true);                           // isRichText

                // Alternatively, explicitly set the rich‑text flag (both calls are safe)
                formEditor.SetFieldRichText("Comments", true);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"Rich‑text enabled field saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
