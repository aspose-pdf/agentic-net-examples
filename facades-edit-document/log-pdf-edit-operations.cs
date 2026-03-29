using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        string logPath = "edit_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect);
            txtAnn.Title = "Note";
            txtAnn.Contents = "Added annotation";
            txtAnn.Color = Aspose.Pdf.Color.Yellow;
            page.Annotations.Add(txtAnn);
            LogOperation(logPath, "Add Text Annotation", inputPath);

            // Replace text using PdfContentEditor
            PdfContentEditor editor = new PdfContentEditor(doc);
            editor.ReplaceText("old text", "new text");
            LogOperation(logPath, "Replace Text", inputPath);

            // Delete first image on page 1
            PdfContentEditor imageEditor = new PdfContentEditor(doc);
            imageEditor.DeleteImage(1, new int[] { 1 });
            LogOperation(logPath, "Delete Image", inputPath);

            doc.Save(outputPath);
        }

        Console.WriteLine("PDF edited and saved to " + outputPath);
    }

    private static void LogOperation(string logFile, string operation, string pdfFile)
    {
        string timestamp = DateTime.Now.ToString("o");
        string entry = string.Format("{0} - {1} - {2}", timestamp, Path.GetFileName(pdfFile), operation);
        using (StreamWriter writer = new StreamWriter(logFile, true))
        {
            writer.WriteLine(entry);
        }
    }
}
