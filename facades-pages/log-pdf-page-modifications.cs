using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open log file and PdfPageEditor within using blocks for deterministic disposal
        using (StreamWriter logWriter = new StreamWriter(logPath, false))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Bound PDF: {inputPath}");

            // ---- Page property modifications ----

            // Zoom (percentage, integer value)
            editor.Zoom = 75; // 75% zoom
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set Zoom = {editor.Zoom}");

            // Rotation (must be 0, 90, 180, 270). Cast enum to int because the property expects an int.
            editor.Rotation = (int)Rotation.on90;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set Rotation = {Rotation.on90}");

            // Page size (e.g., A4)
            editor.PageSize = PageSize.A4;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set PageSize = {editor.PageSize}");

            // Horizontal alignment of original content on the result page
            editor.HorizontalAlignment = HorizontalAlignment.Center;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set HorizontalAlignment = {editor.HorizontalAlignment}");

            // Vertical alignment of original content on the result page
            // The correct enum value is Center (not Middle).
            editor.VerticalAlignmentType = VerticalAlignment.Center;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set VerticalAlignmentType = {editor.VerticalAlignmentType}");

            // Display duration for pages (in seconds)
            editor.DisplayDuration = 5;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set DisplayDuration = {editor.DisplayDuration}");

            // Transition type (use one of the defined constants)
            editor.TransitionType = PdfPageEditor.BLINDH;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set TransitionType = {editor.TransitionType}");

            // Transition duration (in seconds)
            editor.TransitionDuration = 2;
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Set TransitionDuration = {editor.TransitionDuration}");

            // Apply all changes to the document
            editor.ApplyChanges();
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Applied changes");

            // Save the modified PDF
            editor.Save(outputPath);
            logWriter.WriteLine($"{DateTime.UtcNow:u} - Saved modified PDF to {outputPath}");
        }

        Console.WriteLine("Page property modifications have been logged.");
    }
}
