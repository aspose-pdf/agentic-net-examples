using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output041.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Form facade to read existing field names
        var form = new Form();
        form.BindPdf(inputPath);
        bool hasComments = form.FieldNames != null && form.FieldNames.Contains("Comments");

        if (hasComments)
        {
            // FormEditor modifies the structure – it can remove a field
            var editor = new FormEditor();
            editor.BindPdf(inputPath);
            editor.RemoveField("Comments");   // correct method name
            editor.Save(outputPath);
            editor.Close();
        }
        else
        {
            // No "Comments" field – just copy the original PDF
            File.Copy(inputPath, outputPath, true);
        }
    }
}
