FROM jetbrains/teamcity-agent:latest

ADD tuirootca.crt /usr/local/share/ca-certificates/tuirootca.crt
RUN chmod 644 /usr/local/share/ca-certificates/tuirootca.crt && update-ca-certificates