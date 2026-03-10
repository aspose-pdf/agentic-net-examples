using System;

namespace Documentation
{
    /// <summary>
    /// Updated PDF documentation introduction.
    /// </summary>
    public static class PdfDocumentation
    {
        // Replaced the incorrect term “text facades” with the correct namespace Aspose.Pdf.Facades.
        public const string Introduction =
            "Aspose.Pdf.Facades provides a set of high‑level classes that simplify common PDF operations " +
            "such as binding, editing, and saving documents. These facades act as wrappers around the core " +
            "PDF API, allowing developers to work with PDFs without dealing with low‑level details.";
    }

    // Added a minimal entry point so the project compiles as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Simple demonstration – write the introduction to the console.
            Console.WriteLine(PdfDocumentation.Introduction);
        }
    }
}