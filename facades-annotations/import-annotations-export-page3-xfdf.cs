using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "schedule_update.pdf";
        const string importXfdf = "scheduleAdditions.xfdf";
        const string exportXfdf = "page3Export.xfdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXfdf))
        {
            Console.Error.WriteLine($"XFDF not found: {importXfdf}");
            return;
        }

        // Create the annotation editor (Facades API)
        var editor = new PdfAnnotationEditor();

        // Bind the target PDF document
        editor.BindPdf(pdfPath);

        // Import annotations from the XFDF file
        editor.ImportAnnotationsFromXfdf(importXfdf);

        // Export annotations from page 3 to a new XFDF file.
        // Cast null to string[] to disambiguate the overload and export all annotation types.
        using (FileStream fs = File.Create(exportXfdf))
        {
            editor.ExportAnnotationsXfdf(fs, 3, 3, (string[])null);
        }

        // Save the PDF with the newly imported annotations (optional)
        editor.Save(pdfPath);
        editor.Close();

        Console.WriteLine("Annotations imported and page 3 exported successfully.");
    }
}
