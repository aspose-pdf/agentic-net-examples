// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Count annotations before deletion (sum over all pages)
        int countBefore = 0;
        foreach (Page page in editor.Document.Pages)
        {
            countBefore += page.Annotations.Count;
        }

        // Delete all annotations
        editor.DeleteAnnotations();

        // Count annotations after deletion
        int countAfter = 0;
        foreach (Page page in editor.Document.Pages)
        {
            countAfter += page.Annotations.Count;
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close(); // release resources

        Console.WriteLine($"Annotations before deletion: {countBefore}");
        Console.WriteLine($"Annotations after deletion : {countAfter}");
        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file is intentionally left empty. It exists solely to satisfy
// the project file's <Compile Include="AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig" />
// entry, preventing the CS2001 "source file could not be found" error.
// No C# code is required here.
