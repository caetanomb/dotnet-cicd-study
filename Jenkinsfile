node('docker') {

    stage 'Checkout'
        checkout scm

    stage 'Build e UnitTest'
        sh "docker build -t accountownerapp:B${BUILD_NUMBER} -f Dockerfile ."
    stage 'Publish UnitTest Reports'
        containerID = sh (
            script: "docker run -d accountownerapp:B${BUILD_NUMBER}",
        returnStdout: true
        ).trim()
        echo "Container ID is ==> ${containerID}"
        sh "docker cp ${containerID}:/TestResults/test_results.xml test_results.xml"
        sh "docker stop ${containerID}"
        sh "docker rm ${containerID}"
        step([$class: 'MSTestPublisher', failOnError: false, testResultsFile: 'test_results.xml'])
                
    stage 'Integration Test'
        sh "docker-compose -f integrationtests.docker-compose.yml up --force-recreate --abort-on-container-exit"
        sh "docker-compose -f integrationtests.docker-compose.yml down -v"
}