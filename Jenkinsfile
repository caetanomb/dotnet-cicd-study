node('docker') {
    
    stage 'Checkout'
        Checkout scm

    stage 'Build e UnitTest'
        sh "docker build -t accountownerapp:B${BUILD_NUMBER} -f Dockerfile ."

    stage 'Integration Test'
        sh "docker-compose -f Integrationtests.docker-compose.yml up --force-recreate --abort-on-container-exit"
        sh "docker-compose -f Integrationtests.docker-compose.yml down -v"
}