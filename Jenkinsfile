node('docker') {

    stage 'Checkout'
        checkout scm

    stage 'Build e UnitTest'
        sh "docker build -t accountownerapp:B${BUILD_NUMBER} -f Dockerfile ."

    stage 'Integration Test'
        sh "docker-compose -f integrationtests.docker-compose.yml up --force-recreate --abort-on-container-exit"
        sh "docker-compose -f integrationtests.docker-compose.yml down -v"
}