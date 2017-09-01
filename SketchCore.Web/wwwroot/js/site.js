angular
    .module('sketchApp', [])
    .controller('TimerCtrl', function ($scope, $rootScope) {
        var spaceBarHitCount = 0;
        var timer;

        $scope.timeLeft = 0;

        $scope.$on('my:keypress', function (event, keyboardEvent) {
            if (keyboardEvent.charCode === 32 /*space bar*/) {
                event.preventDefault();
                if (++spaceBarHitCount % 2 == 0) {
                    startTimer();
                } else {
                    stopTimer();
                }
            }
        });

        startTimer();

        function startTimer() {
            var currentTime = new Date().getTime();
            timer = setInterval(function () {
                $scope.$apply(function () {
                    var previousTime = currentTime;
                    currentTime = new Date().getTime();
                    $scope.timeLeft = Math.max($scope.timeLeft - currentTime + previousTime, 0);
                    if ($scope.timeLeft === 0) {
                        clearInterval(timer);
                        window.location.href = $scope.nextPageUrl;
                    }

                    $scope.minutes = Math.floor($scope.timeLeft / 1000 / 60);
                    var s = Math.floor($scope.timeLeft / 1000 - $scope.minutes * 60);
                    if ((s + '').length <= 1) s = '0' + s;
                    $scope.seconds = s;
                });
            }, 100);
        }

        function stopTimer() {
            clearInterval(timer);
        }

    });
