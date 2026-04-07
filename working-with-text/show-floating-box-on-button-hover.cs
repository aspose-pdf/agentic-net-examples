using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Create a hidden text annotation that will serve as the floating box
            var floatingRect = new Aspose.Pdf.Rectangle(100, 600, 300, 700);
            TextAnnotation floatingBox = new TextAnnotation(page, floatingRect)
            {
                Contents = "Floating information",
                Color = Aspose.Pdf.Color.LightGray,
                Flags = AnnotationFlags.Hidden // initially hidden
            };
            // Optional border for the floating box
            floatingBox.Border = new Border(floatingBox) { Width = 1 };
            page.Annotations.Add(floatingBox);

            // Create a push button field
            var buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);
            ButtonField button = new ButtonField(page, buttonRect)
            {
                NormalCaption = "Show Box"
            };

            // Assign HideAction to display the floating box when the mouse enters the button area
            button.Actions.OnEnter = new HideAction(floatingBox, false);
            // Optionally hide the box again when the mouse exits
            button.Actions.OnExit = new HideAction(floatingBox, true);

            // Add the button to the form
            doc.Form.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with interactive button: '{outputPath}'.");
    }
}