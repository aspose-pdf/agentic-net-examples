using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfViewerPreferenceHelper
{
    /// <summary>
    /// Toggles the FullScreen viewer preference for a PDF file.
    /// If FullScreen is currently enabled, it will be disabled; otherwise it will be enabled.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    public static void ToggleFullScreen(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Load the PDF using the recommended using pattern (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Retrieve current viewer preferences
            int currentPrefs = editor.GetViewerPreference();

            // FullScreen flag constant (cast enum to int before bitwise operation)
            const int FullScreenFlag = (int)ViewerPreference.PageModeFullScreen;

            // Toggle the flag using bitwise XOR
            int newPrefs = currentPrefs ^ FullScreenFlag;

            // Apply the new viewer preference
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF (save rule)
            editor.Save(outputPdfPath);
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional: run a quick test if arguments are supplied.
        if (args.Length == 2)
        {
            PdfViewerPreferenceHelper.ToggleFullScreen(args[0], args[1]);
            Console.WriteLine("Viewer preference toggled successfully.");
        }
        else
        {
            Console.WriteLine("Usage: <exe> <inputPdfPath> <outputPdfPath>");
        }
    }
}