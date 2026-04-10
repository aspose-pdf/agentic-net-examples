// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: using block for disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options with multi‑page output
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true
                    // Additional options can be set here if needed
                };

                // Save as HTML using explicit SaveOptions (required for non‑PDF formats)
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to multi‑page HTML: {outputHtml}");
        }
        // HTML conversion relies on GDI+; handle Windows‑only limitation gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file is intentionally empty but must exist so that the project
// does not fail with CS2001 (source file not found). It is compiled as
// a C# source file because the project file includes it under the
// <Compile> item group. Providing a minimal valid C# construct satisfies
// the compiler without affecting runtime behaviour.

namespace AsposePdfApi.GeneratedMSBuildEditorConfig
{
    // No members are required – the presence of a valid namespace and
    // an empty class is sufficient for compilation.
    internal static class Placeholder { }
}
