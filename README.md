# Markdown To WinUI3

> [!IMPORTANT]  
> Markdown To WinUI 3 is still in development. Only some of markdown syntaxes are supported for now.

Markdown To WinUI 3 is a lightweight and efficient project designed to render Markdown content into WinUI3 applications. It aims to bridge the gap between markdown documents and Win UI3 apps.

## Features

- **Easy Integration**: Integrate markdown rendering seamlessly into your WinUI 3 applications.
- **Customizable Styles**: Customize the appearance of rendered markdown to fit your application's theme.

## Usage

Here's a simple example of how to use Markdown To WinUI 3 in your application:

```csharp
using MarkdownToWinUi3.MdToWinUi;

// Load your markdown content
string markdownContent = "# Hello, WinUI 3!\nThis is a sample markdown content.";

// Get StackPanel element with formatted markdown content
var MdStackPanel = MdConverter.ConvertMarkdownToStackPanel(markdownContent);

// Add the rendered content to your application's UI
Grid.Children.Add(MdStackPanel);
```

## Supported Syntaxes
Markdown To WinUI 3 supports only some of markdown syntaxes for now. Below is a table listing the currently supported syntaxes:

| **Syntax**                                                               | **Description**         | Supported? |
|--------------------------------------------------------------------------|-------------------------|------------|
| `# Header 1`                                                             | Heading level 1         |      ✅     |
| `## Header 2`                                                            | Heading level 2         |      ✅     |
| `### Header 3`                                                           | Heading level 3         |      ✅     |
| `#### Header 4`                                                          | Heading level 4         |     ✅*     |
| `##### Header 5`                                                         | Heading level 5         |     ✅*     |
| `###### Header 6`                                                        | Heading level 6         |     ✅*     |
| `**bold**`                                                               | Bold text               |      ✅     |
| `*italic*`                                                               | Italic text             |      ✅     |
| `***bold italic***`                                                      | Bold and italic text    |      ✅     |
| `- List item`                                                            | Unordered list item     |      ❌     |
| `1. List item`                                                           | Ordered list item       |      ❌     |
| `[Link](url)`                                                            | Hyperlink               |      ❌     |
| `![Image](url)`                                                          | Image                   |      ❌     |
| `` `code` ``                                                             | Inline code             |      ❌     |
| ` ```code``` `                                                           | Code block              |      ❌     |
| `> Blockquote`                                                           | Blockquote              |      ❌     |
| `---`, `***`, `---`                                                      | Horizontal rule         |      ❌     |
| `\|---\|---\|`[*](https://www.markdownguide.org/extended-syntax/#tables) | Table                   |      ❌     |
| `~~strikethrough~~`                                                      | Strikethrough           |      ❌     |
| `^superscript^`                                                          | Superscript (extension) |      ❌     |
| `~subscript~`                                                            | Subscript (extension)   |      ❌     |
| `:emoji:`                                                                | Emoji (extension)       |      ❌     |
| `[^Footnote]`                                                            | Footnote (extension)    |      ❌     |


## Contributing
Contributions are welcome! If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are warmly welcome.

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Push to the branch.
5. Create a new Pull Request.
