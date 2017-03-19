var gulp        = require('gulp'),
    concat      = require('gulp-concat'),
    uglify      = require('gulp-uglify'),
    rename      = require('gulp-rename'),
    notify      = require('gulp-notify');

gulp.task('minificar-js', function() {
    return gulp.src(['Scripts/app/services/*.js', 'Scripts/app/controllers/*.js'])
        .pipe(concat('bundle'))
        .on('error', notify.onError("Error: <%= error.message %>"))
        .pipe(uglify())
        .on('error', notify.onError("Error: <%= error.message %>"))
        .pipe(rename({
            extname: ".min.js"
        }))
        .pipe(gulp.dest('Scripts/app/'))
        .pipe(notify('Minificacao e concatenacao de arquivos JavaScript terminada!'));
});


