/// <binding ProjectOpened='watch-sass' />
// JavaScript source code
var gulp = require("gulp"),
    fs = require("fs"),
    sass = require("gulp-sass");

var paths = {
    webroot: "./wwwroot/"
}
paths.scss ="Styles/**/*.scss";

gulp.task('sass', function () {
    gulp.src(paths.scss)
        .pipe(sass())
        .pipe(gulp.dest(paths.webroot + "css"));
});

gulp.task('watch-sass', function () {
    gulp.watch(paths.scss, ['sass']);
})