var fs = require('fs');
var path = require('path');
var merge = require('merge-stream');
var gulp = require('gulp');
var concat = require('gulp-concat');
var rename = require('gulp-rename');
var uglify = require('gulp-uglify');
var bump = require("gulp-bump");
var templateCache = require('gulp-angular-templatecache');
var sass = require("gulp-sass");

var paths = {
	webroot: "./wwwroot/"
};
var cmsPath = paths.webroot + "app/cms/js";
var cssPath = paths.webroot + "app/cms/css";

function getFolders(dir) {
	return fs.readdirSync(dir)
		.filter(function (file) {
			return fs.statSync(path.join(dir, file)).isDirectory();
		});
}

gulp.task("sass", function () {
	return gulp.src(cssPath + "/main.scss")
	  .pipe(sass())
	  .pipe(gulp.dest(cssPath));
});

gulp.task('minify', function () {
	var cmsFolders = getFolders(cmsPath);

	var cms = cmsFolders.map(function (folder) {
		return gulp.src(path.join(cmsPath, folder, '/**/*.js'))
			// concat into foldername.js
			.pipe(concat(folder + '.js'))
			// write to output (not needed for now)
			//.pipe(gulp.dest(cmsPath))
			// minify
			.pipe(uglify())
			// rename to folder.min.js
			.pipe(rename(folder + '.min.js'))
			// write to output again
			.pipe(gulp.dest(cmsPath));
	});

	return merge(cms);
});

// Increment version.
gulp.task("bump", function () {
	gulp.src("./project.json")
	.pipe(bump())
	.pipe(gulp.dest("./"));
});

//gulp.task('templates', function () {
//	return gulp.src(paths.webroot + 'app/frontend/views/**/*.html')
//	  .pipe(templateCache('templates.min.js', { module: 'frontend', root: 'app/frontend/views' }))
//	  .pipe(gulp.dest(paths.webroot + 'app/frontend/js'));
//});