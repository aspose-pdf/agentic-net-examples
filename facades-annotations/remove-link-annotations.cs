using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        string[] annotationTypes = new string[] { "Link" };
        IList<Annotation> linkAnnotations = editor.ExtractAnnotations(3, 7, annotationTypes);

        foreach (Annotation annotation in linkAnnotations)
        {
            if (!string.IsNullOrEmpty(annotation.Name))
            {
                editor.DeleteAnnotation(annotation.Name);
            }
        }

        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine("Link annotations removed from pages 3-7 and saved to '" + outputPath + "'.");
    }
}