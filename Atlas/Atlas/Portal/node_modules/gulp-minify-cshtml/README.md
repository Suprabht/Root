# gulp-minify-cshtml [![NPM version][npm-image]][npm-url]
Minify ASP.NET Razor Views (.cshtml files).

## Usage

First, install `gulp-minify-cshtml` as a development dependency:

```shell
npm install --save-dev gulp-minify-cshtml
```

Then, add it to your `gulpfile.js`:

### Simple
```javascript
var minifyCshtml = require('gulp-minify-cshtml');

gulp.task('minify-cshtml', function(){
  gulp.src('./Views/**/(!(*.min.cshtml)|*.cshtml)')
    .pipe(minifyCshtml())
    .pipe(gulp.dest('./Views/**/*.min.cshtml'));
});
```
### Advanced
```javascript
var minifyCshtml = require('gulp-minify-cshtml');

gulp.task('minify-cshtml', function(){
  gulp.src('./Views/**/(!(*.min.cshtml)|*.cshtml)')
    .pipe(minifyCshtml({
      comments: true,       // Remove HTML comments <!-- -->
      razorComments: true,  // Remove Razor comments @* *@
      whitespace: true      // Remove white-space
    }))
    .pipe(gulp.dest('./Views/**/*.min.cshtml'));
});
```


## API

gulp-replace can be called with an optional 'options' object.

### minifyCshtml(options)

#### options
Type: `Object`

##### options.comments
Type: `boolean`  
Default: `false`

Remove HTML comments `<!-- -->`

##### options.razorComments
Type: `boolean`  
Default: `true`

Remove Razor comments `@* *@`

##### options.whitespace
Type: `boolean`  
Default: `true`

Remove white-space between angle brackets `> <`

## Known Issues and Limitations

This plugin is not doing proper Razor and HTML parsing (That would be a very big project). It uses simple regular expressions to do the replacements which works in all cases except those unlikely cases listed below.

If you have strings in your Razor C# code like the two lines below, they will be wrongly modified. The workaround is to split up the string.

```
string a = ">   <"; // Will be changed to "><". Use "> " + " <" instead.
string b = "@* *@"; // Will be changed to "". Use "@* " + " *@" instead.
```

[npm-url]: https://npmjs.org/package/gulp-minify-cshtml
[npm-image]: https://badge.fury.io/js/gulp-minify-cshtml.svg
