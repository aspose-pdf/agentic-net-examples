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
        const string submitButtonName = "btnSubmit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // JavaScript that validates required fields and then submits the form
        string javaScript = "var required = [\"Name\",\"Email\"];" +
                            "for (var i = 0; i < required.length; i++) {" +
                            "    var f = this.getField(required[i]);" +
                            "    if (!f.value) {" +
                            "        app.alert(\"Please fill the required field: \" + required[i]);" +
                            "        return false;" +
                            "    }" +
                            "}" +
                            "this.submitForm({cURL:\"https://www.example.com/submit\"});";

        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            bool scriptSet = formEditor.SetFieldScript(submitButtonName, javaScript);
            if (!scriptSet)
            {
                Console.Error.WriteLine("Failed to set JavaScript on the submit button.");
            }
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
