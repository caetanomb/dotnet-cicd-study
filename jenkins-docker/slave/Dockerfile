FROM ubuntu:18.04

RUN apt-get update && \
    apt-get install -y apt-transport-https ca-certificates curl wget gnupg-agent software-properties-common

ADD domainca.crt /usr/local/share/ca-certificates/domainca.crt
RUN chmod 644 /usr/local/share/ca-certificates/domainca.crt && update-ca-certificates

# Install Docker CLI and docker-compose in the agent
# Curls was replaced with wget due to firewall policies
RUN wget --output-document=gpg https://download.docker.com/linux/ubuntu/gpg 
RUN apt-key add gpg
RUN add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" && \
    apt-cache policy docker-ce && \
    apt-get update && \
    apt-get install -y docker-ce && \    
    wget --output-document=/usr/local/bin/docker-compose https://github.com/docker/compose/releases/download/1.24.1/docker-compose-Linux-x86_64 && chmod +x /usr/local/bin/docker-compose

RUN apt-get install -y openjdk-8-jre python python-pip git
RUN pip install jenkins-webapi

# Create work directories
RUN mkdir -p /home/jenkins
RUN mkdir -p /var/lib/jenkins

# Start-up script to attach the slave to the master
ADD slave.py /var/lib/jenkins/slave.py

WORKDIR /home/jenkins

ENV JENKINS_URL "http://jenkins"
ENV JENKINS_SLAVE_ADDRESS ""
ENV JENKINS_USER "admin"
ENV JENKINS_PASS "admin"
ENV SLAVE_NAME ""
ENV SLAVE_SECRET ""
ENV SLAVE_EXECUTORS "1"
ENV SLAVE_LABELS "docker"
ENV SLAVE_WORING_DIR ""
ENV CLEAN_WORKING_DIR "true"

CMD [ "python", "-u", "/var/lib/jenkins/slave.py" ]