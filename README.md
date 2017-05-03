## Utube

A library written in C# to work with Youtube videos and playlists. It works by extracting information from the HTML source of the original webpage.

And is also under construction and needs some polishing.

### Videos
To retrieve information from a Youtube video you use the `YoutubeVideo` class. Examples:

```csharp
  /*
    To retrieve information of a Youtube video using its identifier.
  */
  var video1 = new YoutubeVideo("lqj-QNYsZFk");

  /*
    To retrieve information of a Youtube video using its URL.
  */
  var video2 = new YoutubeVideo("https://www.youtube.com/watch?v=lqj-QNYsZFk");

  /*
    You can then use the YoutubeVideo instance's properties to get the information such as:
  */
  Console.WriteLine(video1.Title);
  Console.WriteLine(video1.Description);
  Console.WriteLine(video1.Views);
  Console.WriteLine(video1.Length);
  Console.WriteLine(video1.Id);
```

### Playlists
To retrieve information from a Youtube playlist you can use the `YoutubePlaylist` class.
