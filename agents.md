# Aspose.PDF for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.PDF for .NET API.

## Repository Overview

This repository contains **1402** working code examples demonstrating Aspose.PDF for .NET capabilities.

**Statistics** (as of 2026-03-10):
- Total Examples: 1402
- Categories: 22

## Category Details

### Conversion
- Examples: 230
- Guide: [agents.md](./conversion/agents.md)

### Document
- Examples: 72
- Guide: [agents.md](./document/agents.md)

### JavaScript
- Examples: 9
- Guide: [agents.md](./javascript/agents.md)

### Operators
- Examples: 7
- Guide: [agents.md](./operators/agents.md)

### Pages
- Examples: 45
- Guide: [agents.md](./pages/agents.md)

### Stamping
- Examples: 17
- Guide: [agents.md](./stamping/agents.md)

### accessibility-and-tagged-pdfs
- Examples: 70
- Guide: [agents.md](./accessibility-and-tagged-pdfs/agents.md)

### basic-operations
- Examples: 78
- Guide: [agents.md](./basic-operations/agents.md)

### compare-pdf
- Examples: 30
- Guide: [agents.md](./compare-pdf/agents.md)

### facades-acroforms
- Examples: 35
- Guide: [agents.md](./facades-acroforms/agents.md)

### facades-texts-and-images
- Examples: 22
- Guide: [agents.md](./facades-texts-and-images/agents.md)

### graphs-zugferd-operators
- Examples: 9
- Guide: [agents.md](./graphs-zugferd-operators/agents.md)

### parse-pdf
- Examples: 76
- Guide: [agents.md](./parse-pdf/agents.md)

### securing-and-signing-pdf
- Examples: 25
- Guide: [agents.md](./securing-and-signing-pdf/agents.md)

### working-with-annotations
- Examples: 252
- Guide: [agents.md](./working-with-annotations/agents.md)

### working-with-attachments
- Examples: 95
- Guide: [agents.md](./working-with-attachments/agents.md)

### working-with-forms
- Examples: 74
- Guide: [agents.md](./working-with-forms/agents.md)

### working-with-graphs
- Examples: 33
- Guide: [agents.md](./working-with-graphs/agents.md)

### working-with-images
- Examples: 27
- Guide: [agents.md](./working-with-images/agents.md)

### working-with-tables
- Examples: 118
- Guide: [agents.md](./working-with-tables/agents.md)

### working-with-text
- Examples: 62
- Guide: [agents.md](./working-with-text/agents.md)

### working-with-xml
- Examples: 16
- Guide: [agents.md](./working-with-xml/agents.md)

## Code Conventions

### Explicit Types (No `var`)
Always use explicit type declarations. Never use `var`.

```csharp
// CORRECT
Document document = new Document("input.pdf");
Page page = document.Pages[1];
TextFragmentAbsorber absorber = new TextFragmentAbsorber("search");

// WRONG
// var document = new Document("input.pdf");
// var page = document.Pages[1];
```

### One-Based Indexing
Aspose.PDF uses 1-based indexing for Pages, Annotations, and EmbeddedFiles.

```csharp
// CORRECT — first page is index 1
Page firstPage = document.Pages[1];
Annotation firstAnnotation = page.Annotations[1];
FileSpecification firstFile = document.EmbeddedFiles[1];

// WRONG — index 0 throws IndexOutOfRangeException
// Page page = document.Pages[0];
```

### Fully Qualified Ambiguous Types
When using both `Aspose.Pdf` and `Aspose.Pdf.Drawing`, qualify ambiguous types.

```csharp
// CORRECT
Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 300, 400);
Aspose.Pdf.Drawing.Rectangle drawRect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);
Aspose.Pdf.Color pdfColor = Aspose.Pdf.Color.Blue;

// WRONG — ambiguous CS0104
// Rectangle rect = new Rectangle(100, 200, 300, 400);
// Color color = Color.Blue;
```

### Resource Cleanup
Always use `using` blocks or explicit `Dispose()` for IDisposable objects.

```csharp
// CORRECT
using (Document document = new Document("input.pdf"))
{
    // work with document
    document.Save("output.pdf");
}
```

### Save Pattern
Always save the document after modifications.

```csharp
Document document = new Document("input.pdf");
// ... make modifications ...
document.Save("output.pdf");
```

## Common Mistakes (Anti-Patterns)

These are verified mistakes that cause build failures. **Never** use the wrong patterns.

### Prefer Fully Qualified Names For Ambiguous Types
When a type name can resolve to multiple namespaces (especially with Aspose.Pdf, Aspose.Pdf.Drawing, System.IO, and System.Drawing), ALWAYS use fully qualified names for ANY type that exists in more t

```csharp
// WRONG
Rectangle rect = new Rectangle(100, 500, 300, 550); // CS0104
Path.GetFileName("input.pdf");                      // CS0104
Image img = Image.FromFile("photo.jpg");             // CS0104
Color c = Color.Blue;                                // CS0104
```

```csharp
// CORRECT
Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
System.IO.Path.GetFileName("input.pdf");
Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image();
System.Drawing.Image sysImg = System.Drawing.Image.FromFile("photo.jpg");
Aspose.Pdf.Color pdfColor = Aspose.Pdf.Color.Blue;
System.Drawing.Color sysColor = System.Drawing.Color.Blue;
Aspose.Pdf.Point pdfPoint = new Aspose.Pdf.Point(100, 200);
// ...
```

### Annotation Collection One Based Indexing
Aspose.Pdf annotation collections use 1-based indexing, same as Pages. page.Annotations[1] is the first annotation. page.Annotations[0] throws an exception. page.Annotations.Delete(index) also expects

```csharp
// WRONG
page.Annotations[0]; // throws IndexOutOfRangeException
page.Annotations.Delete(page.Annotations.Count - 1); // deletes second-to-last
```

```csharp
// CORRECT
for (int i = 1; i <= page.Annotations.Count; i++)
{
    Annotation ann = page.Annotations[i];
}
page.Annotations.Delete(page.Annotations.Count);
```

### Annotation Title Requires Markup Annotation
The 'Title' property belongs to MarkupAnnotation, NOT the base Annotation class. When iterating page.Annotations or using FindByName(), the returned type is Annotation which does not have Title. You m

```csharp
// WRONG
Annotation ann = page.Annotations[1];
Console.WriteLine(ann.Title); // CS1061
```

```csharp
// CORRECT
Annotation ann = page.Annotations[1];
string title = ann is MarkupAnnotation markup ? markup.Title : "N/A";
Console.WriteLine($"Title={title}, Contents={ann.Contents}");
Annotation retrieved = page.Annotations.FindByName("MyNote");
if (retrieved is MarkupAnnotation ma)
{
    Console.WriteLine(ma.Title);
}
```

### Border Requires Parent Annotation
The Border class has no parameterless constructor — it requires an Annotation parent argument: new Border(annotation). Border also has NO Color property; border color is controlled by the annotation's

```csharp
// WRONG
var ann = new TextAnnotation(page, rect)
{
    Border = new Border { Width = 1, Color = Aspose.Pdf.Color.Black }
};
Errors: CS1729, CS1061
var ann = new TextAnnotation(page, rect)
...
```

```csharp
// CORRECT
TextAnnotation ann = new TextAnnotation(page, rect)
{
    Title = "Note",
    Contents = "Hello",
    Color = Aspose.Pdf.Color.Yellow
};
ann.Border = new Border(ann) { Width = 1 };
```

### Qualify Rectangle And Color
Always use fully qualified Aspose.Pdf.Rectangle and Aspose.Pdf.Color to avoid ambiguity with System.Drawing types. Even if System.Drawing is not explicitly referenced, implicit usings or transitive de

```csharp
// WRONG
Rectangle rect = new Rectangle(100, 500, 300, 550);
txtAnn.Color = Color.Yellow;
```

```csharp
// CORRECT
Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
txtAnn.Color = Aspose.Pdf.Color.Yellow;
```

### Mhtsaveoptions Namespace Missing
The MhtSaveOptions class is not in the Aspose.Pdf namespace — it resides in Aspose.Pdf.Facades. You must add 'using Aspose.Pdf.Facades;' and ensure the Aspose.PDF.dll reference includes the Facades as

```csharp
// WRONG
using Aspose.Pdf; // alone is insufficient
MhtSaveOptions mhtOptions = new MhtSaveOptions(); // ❌ CS0246
```

```csharp
// CORRECT
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // ← Required for MhtSaveOptions
MhtSaveOptions mhtOptions = new MhtSaveOptions
{
    PartsEmbeddingMode = MhtSaveOptions.PartsEmbeddingModes.EmbedAllIntoMht
};
```

### Caret Annotation Symbol Missing
The enum 'CaretAnnotationSymbol' does not exist in Aspose.Pdf.Annotations. The 'Symbol' property of 'CaretAnnotation' expects a value of type 'Aspose.Pdf.Annotations.CaretSymbol', which is an enum def

```csharp
// WRONG
Symbol = CaretAnnotationSymbol.Insert // ❌ enum does not exist
```

```csharp
// CORRECT
using Aspose.Pdf.Annotations;
...
CaretAnnotation caret = new CaretAnnotation(page, rect)
{
    Symbol = CaretSymbol.Insert,
    Contents = "...",
    Color = Aspose.Pdf.Color.Red
};
```

### Fix Texfragment Margin And Color Ambiguity
The TeXFragment class does not have a 'Margin' property; instead, use 'TopMargin', 'BottomMargin', etc. Also, 'Color' is ambiguous between System.Drawing.Color and Aspose.Pdf.Color — use fully qualifi

```csharp
// WRONG
TeXFragment texFragment = new TeXFragment(...);
texFragment.Margin = new Margin { Top = 20, Bottom = 20 }; // ❌ No such property
DefaultAppearance appearance = new DefaultAppearance(..., Color.Black); // ❌ ambiguous
```

```csharp
// CORRECT
TeXFragment texFragment = new TeXFragment(@"\frac{a}{b} = c", true);
texFragment.HorizontalAlignment = HorizontalAlignment.Center;
texFragment.TopMargin = 20;
texFragment.BottomMargin = 20;
DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);
```

### Resolve Color Ambiguity In Defaultappearance
When constructing DefaultAppearance, use fully qualified Aspose.Pdf.Color (not System.Drawing.Color) to avoid ambiguity. The constructor expects Aspose.Pdf.Color, and System.Drawing.Color is not impli

```csharp
// WRONG
DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Color.Black); // ❌ ambiguous
```

```csharp
// CORRECT
DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);
```

### Resolve Rectangle Ambiguity And Fix Constructor Types
The error CS0104 occurs because both Aspose.Pdf.Drawing.Rectangle and Aspose.Pdf.Rectangle exist, causing ambiguity. Use fully qualified name Aspose.Pdf.Drawing.Rectangle for drawing shapes. Also, Gra

```csharp
// WRONG
Graph graph = new Graph(500, 800); // obsolete & wrong types
Rectangle rect = new Rectangle(...); // ambiguous
new Ellipse(300, 600, 150, 100); // double literals → CS0664
```

```csharp
// CORRECT
Graph graph = new Graph(500.0, 800.0); // Use double literals
// Use fully qualified name for drawing shapes
Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50.0F, 600.0F, 200.0F, 100.0F);
Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(300.0F, 600.0F, 150.0F, 100.0F);
```

## Domain Knowledge

Cross-cutting rules that apply across multiple categories.

- **Save the modified {doc} to {output_pdf}.**
  _(Applies to: Annotations, Stamps-Watermarks, Tables (+1 more))_
- **Persist the changes by calling {doc}.Save({output_pdf}).**
  _(Applies to: Links-Actions, Pages, Stamps-Watermarks)_
- **Persist the PDF by calling {doc}.Save({output_pdf}).**
  _(Applies to: DocumentConversion, Images, Text)_
- **Load a PDF document: Document {doc} = new Document({input_pdf});**
  _(Applies to: Annotations, Forms, Images)_
- **Load a PDF into {doc} using new Document({input_pdf}).**
  _(Applies to: Bookmarks, Links-Actions, Security-Signatures)_
- **Save the modified document to {output_pdf}.**
  _(Applies to: Annotations, Forms, Tables)_
- **Load a PDF by creating a {doc} = new Document({input_pdf});**
  _(Applies to: DocumentConversion, Stamps-Watermarks)_

## Command Reference

### Build and Run
```bash
# Create a new project (if needed)
dotnet new console -n ExampleProject --framework net10.0

# Add Aspose.PDF NuGet package
dotnet add package Aspose.PDF --version 26.2.0

# Build
dotnet build --configuration Release --verbosity minimal

# Run
dotnet run
```

### Project File (.csproj)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Aspose.PDF" Version="26.2.0" />
  </ItemGroup>
</Project>
```

### Environment
- .NET SDK: 10.0 or higher
- NuGet: Aspose.PDF 26.2.0
- All examples are standalone Console Applications
- Each `.cs` file can be compiled and run independently

## How to Use These Examples

### Prerequisites
- .NET SDK (10.0 or higher)
- Aspose.PDF for .NET (26.2.0 or higher)
- NuGet package restore enabled

### Running an Example
1. Navigate to any category folder
2. Each .cs file is a standalone Console Application
3. Ensure `input.pdf` exists in the same directory (where required)
4. Compile and run:
   ```bash
   dotnet run <example-file.cs>
   ```

<!-- AUTOGENERATED:START -->
Updated: 2026-03-10 | Run: `20260310_160245_96a7ea`
<!-- AUTOGENERATED:END -->

---

*This repository is maintained by automated code generation. Last updated: 2026-03-10 | Total examples: 1402*
